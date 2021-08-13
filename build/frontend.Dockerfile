ARG NODE_IMAGE_BASE=14-alpine
ARG BUILD_INFO="No build info"
ARG VERSION=0.0.1

# =====================================================================
# Stage 1 - Install dependencies only when needed
# =====================================================================
FROM node:$NODE_IMAGE_BASE AS deps
# Check https://github.com/nodejs/docker-node/tree/b4117f9333da4138b03a546ec926ef50a31506c3#nodealpine to understand why libc6-compat might be needed.
RUN apk add --no-cache libc6-compat
WORKDIR /app
COPY frontend/package.json frontend/yarn.lock ./
RUN yarn install --frozen-lockfile

# =====================================================================
# Stage 2 - Rebuild the source code only when needed
# =====================================================================
FROM node:$NODE_IMAGE_BASE AS builder
WORKDIR /app
COPY frontend/ ./
COPY --from=deps /app/node_modules ./node_modules
RUN yarn build && yarn install --production --ignore-scripts --prefer-offline

# =====================================================================
# Stage 3 - Production image, copy all the files and run next
# =====================================================================
FROM node:$NODE_IMAGE_BASE AS runner
WORKDIR /app

ENV NODE_ENV production

RUN addgroup -g 1001 -S nodejs
RUN adduser -S nextjs -u 1001

# You only need to copy next.config.js if you are NOT using the default configuration
COPY --from=builder /app/next.config.js ./
COPY --from=builder /app/public ./public
COPY --from=builder --chown=nextjs:nodejs /app/.next ./.next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package.json ./package.json

USER nextjs
EXPOSE 3000

# !IMPORTANT! Set and override API_ENDPOINT at runtime
ENV API_ENDPOINT http://localhost:5000/api
ENV VERSION $VERSION
ENV BUILD_INFO $BUILD_INFO
ENV NEXT_TELEMETRY_DISABLED 1

CMD ["yarn", "start"]
