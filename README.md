# Simple News Reader
This is a simple asp.net core web API project that get page number of news and read news datas like id, title, description and etc from that page and add them to database by comparing exist news ids with new news ids.

This project create base on clean architecture and use this technologies:

  * .NET 6
  * Entity framework core 6
  * AutoMapper
  * FluentValidation
  * XUnit
  * Docker
  * Docker Compose

To run this project on docker go to src folder and run this command:

    docker-compose build
  
After get successful message from docker build command run this command:

    docker-compose up
    
Now browse this url https://localhost:5001 or http://localhost:5000 to test application.
