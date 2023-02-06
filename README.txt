# PangeaAssignment

In this project I have 3 end points. Firstly, in a Left and Right End points I am Decdoing Inputs, which user passed. in the last end point, I am comparing
if values, that user passed were correct or not. the code itself have to recive correct base64 inputs, but in case of incorectly passing the values, tere is
ValidatorHelper, that checks possible errors and returns to user what went wrong. Also in Program.cs file there is injected costume midlware, that handles exceptions.
Code testing itself, is possible from swager, as well from its integration tests.

Also, to check if the code works properly or not, there is given integration tests that allow user to check if the code is running properly or not.
In case on your local machine, the swager IP address is different then 44307, you should also change it in Integration tests, LeftUrl, RightUrl and CompareUrl.
