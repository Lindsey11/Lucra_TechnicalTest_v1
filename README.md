# Lucra TechnicalTest

# Set up instructions
1. First create a local instance of the MSSQL Database on your local machine. The script for the spinning up the base can be found in the assets folder within the API project
2. Update the database connection string found the appsettings.json file in the API project to match connection properties requried for connection to your newly crated database. IE server name.
3. In visual studio make sure both the UI project and the API project as both selected as start up projects when debugging. Example below
     ![image](https://github.com/Lindsey11/Lucra_TechnicalTest_v1/assets/32640512/bb7eceac-6db7-474d-9f31-cdae0fcabbd7)

#API usage
1. All get requests for the for images will require a valid JSON bearer token be sent with each request. A token can be generated at the token/get-token end point
2. All documentation can found here: https://documenter.getpostman.com/view/3716236/2s9Ykrcfjm

#UI Usage
