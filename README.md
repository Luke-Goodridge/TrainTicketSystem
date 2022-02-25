# Train Ticket System

## Summary

A small console based project in which you can book train tickets on a train. Written in c# and fully object oriented. I hope to add to it over time expanding it to allow for multiple trains with departure times and all sorts.

### The flow is as follows:

![Main menu](/TrainTicketSystem/images/mainMenu.PNG)
<br />
You can view the ticket prices. These menus are modular and are all using the Menu class, currently 2 types of menu, a navigation and a display.<br />
![Ticket view](/TrainTicketSystem/images/ticketMenu.PNG) <br />
You can get an overall view of the seats on the train. The way trains were implemented, any size train can be made using my Train class <br />
e.g `Train train = new Train(60, 20);` 
<br />would be 60 seats full, and 20 seats per carriage.
<br />
<br />
![Seat view](/TrainTicketSystem/images/seatView.PNG)
<br />
<br />
Here you can look up a seats price, if its taken (red) then you cant book it. First class seats are also more expensive than normal.
