# HadasimProject
1. How to use:

when  running the project the following window will open:

![image](https://github.com/adiRozental/HadasimProject/assets/92113102/0d5fe7a0-87cd-47f8-b69c-844086e1b103)
you can choose each api to use and activate the different get and post options.
for example:
In memberAPI click on Post and then click on the Try it now button:

![image](https://github.com/adiRozental/HadasimProject/assets/92113102/789ed80d-575d-4150-bd62-3607e5e80a2d)

then insert values accordingly, and click on execute. for example:

![image](https://github.com/adiRozental/HadasimProject/assets/92113102/41ba6341-caff-4249-9d22-5d66939e83b4)

and as you can see the member was added and we received code 201.
now you can try the Get/Member/{id} method the same way and you will get the member details:
 
![image](https://github.com/adiRozental/HadasimProject/assets/92113102/84dc41c0-d970-4da0-9d19-04df349e64f9)

you can also try Get/Member and receive all the memebers in the database.

Do the same in order to activate the rest.

2. In order to use this project you should have Visual Studio 2019, using the project type of: asp.net core web api
 and install those packages:
![image](https://github.com/adiRozental/HadasimProject/assets/92113102/ceb1e30e-0f71-468f-84b9-5c532cbb6882)
 
 You will also need mysql server as a database, notice to have the following versions:
 
![image](https://github.com/adiRozental/HadasimProject/assets/92113102/d4b86acc-abe0-478c-9c69-59f9049df18c)

 and create the tables:
CREATE TABLE Member (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Birthdate DATE NOT NULL,
    City VARCHAR(50) NOT NULL,
    Street VARCHAR(50) NOT NULL,
    Number VARCHAR(10) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    smartPhone VARCHAR(20) NOT NULL
);

CREATE TABLE VaccineDetails (
    MemberID INT NOT NULL,
    Date DATE NOT NULL,
    Producer VARCHAR(50) NOT NULL,
    Primary key(MemberID, Date)
    FOREIGN KEY (MemberID) REFERENCES Member(ID)
);

CREATE TABLE IllnessDetails (
    MemberID INT NOT NULL,
    illDate DATE NOT NULL,
    RecoveryDate DATE NOT NULL,
    primary key(MemberID, illDate)
    FOREIGN KEY (MemberID) REFERENCES Member(ID)
);

it should look like that:
![image](https://github.com/adiRozental/HadasimProject/assets/92113102/b6a341d2-fcbf-47f8-9836-39dfc2216c00)
