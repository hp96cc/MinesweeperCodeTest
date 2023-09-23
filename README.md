# Minesweeper Code Test

**Overview**

Whilst this is a console app, the project has been designed so that it could work with any different UI platform. Any UI would just need to implment the 'IPlatform' interface. The reason for this approach is to remove UI dependencies from the game logic. 

**Notes:**

1. Unit Tests are just a sample. They are not exhasutive and further work would be required.
2. Ideally the project would have been implemented using Dependency Injection and all the classes would have been bound by interfaces. This would have cleaned up some of the code and made the Unit Tests easier to write. 
