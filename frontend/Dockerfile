FROM johnpapa/angular-cli AS build
WORKDIR /src/Chat
COPY package.json .
RUN npm i
COPY . .
RUN npm run build

FROM nginx
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /src/Chat/dist /usr/share/nginx/html
