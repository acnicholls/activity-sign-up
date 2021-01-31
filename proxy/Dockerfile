# use NGINX as a reverse proxy
FROM nginx:latest

# copy our configuration file
COPY ./nginx.prod.conf /etc/nginx/conf.d/default.conf

# copy our includes file
COPY ./includes.conf /etc/nginx/conf.d/proxy.conf

