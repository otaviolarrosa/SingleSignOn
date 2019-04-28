
To run this solution locally in your machine, please take care that you have the following tools installed:
- MongoDb
- Redis

Or, if you have docker installed, please run this commands:

$ docker run -d -p 27017:27017 -p 28017:28017 -e AUTH=no mongo:latest
$ docker run -d -p 6379:6379 -i -t redis:3.2.5-alpine

If you have any troubles while trying to setup docker to this solution, please, reset your containers with this:

$ docker stop $(docker ps -a -q)
$ docker rm $(docker ps -a -q)
$ docker rmi $(docker images -a -q)

And run the commands to setup the necessary tools to project above.