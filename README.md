# Vic Stelmak Sample Microservices App
<div align="justify">
	My (AI was not used during the creation of this application!) purely experimental C# application imitating work of a rather small store of 
	miscellaneous items. The following technologies are being used in this test application:
</div>
<br>
<p>
	- Asp.Net Core,<br>
	- Asp.Net Core Identity,<br>
	- Asp.Net Core Integrated Logging,<br>
	- Blazor Wasm,<br>
	- Dapper,<br>
	- Docker,<br>
	- Entity Framework,<br>
	- Fluent Validation,<br>
	- Linq,<br>
	- Mapster,<br>
	- MassTransit,<br>
	- MediatR,<br>
	- Minimal Api,<br>
	- Moq,<br>
	- NuGet Packages (created some packages of my own and uploaded them to my Github Packages section to be used in this project),<br>
	- PostgreSql,<br>
	- RabbitMq,<br>
	- Redis,<br>
	- xUnit,<br>
	- Yarp.
</p>

Also I used the following software architectural patterns during the creation of this application: 
<p>
	- Clean Architecture,<br>
	- Cqrs,<br>
	- Microservices,<br>
	- Proxy,<br>
	- Vertical Slices Architecture.
</p>

In addition, the following software design patterns were used and taken into consideration:
<p>
	- Database per Service (each microservice use it's own database),<br>
	- Repository,<br>
	- Saga,<br>
	- Solid.	
</p>
<div align="justify">
Please take heed that during the initial migration application creates user with login funky@email.com and with password A@12345b and this user is in 
role with maximum permissions (Administrator). Overall, in this application there are three roles: Administrator, Customer and User. Application uses 
role based access control system and that is why only user in role Administrator can see all three panels on the left side. To compile application requires valid connection to User microservice database.  
There are following panels in this application (from top to bottom): Home (basic Blazor page with information about application and it's purpose), 
Catalogue (page where user can purchase stuff and user in Administrator role can add new products and update already existing ones), 
Orders Management (user can see his orders and delete them but only user in Administrator role and user in User role can see and delete any orders 
and can mark them as delivered), Users Management (page only for users in Administrator role allowing to create new users, to delete already existing 
ones and to update them). Even anonymous user can purchase items without logging in and they can even add items to basket (which I actually called 
Orders Management) for other users (I made this feature just for the sake of experimentation and probably this is not a good idea for some real store). 
</div>