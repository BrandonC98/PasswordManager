# Project Definition of Done

The Project is consider done once the application can safely and securely store and display passwords and details about the website associated with the password for individual users of the software. 

# Project Goal

This Project aims to provide a secure application for storing passwords for websites

# Project Retrospective

I learned methods to securly store data, gained a better understanding of how test driven development is applied to a project as well adding more depth to my knowldege of how to apply entity framework. 

In future for a project of this type I would use a Database first approach as it makes a bit more sense for the data I'm working with. I would also set aside more type for the frontend development of the application 

# User Guide

### loggin in
1.1 - if the user doesn't have an account they need to click the "Sign up" button on the top left. If they do then skip to step 1.3

1.2 - the user needs to provide a first name, last name, email address and master password in the appropriate text boxes. once done click the create button. if sucessful a message box will appear telling the user their account was created and it will then take them to the login screen.

1.3 - the user needs to type in their email and master password for their account. If done correctly they will be taken to window which lists all websites the user has created a entry for.

### Creating a new entry

2.1 - Once logged in the user should see a "New Entry" button on the top left. Once clicked places for the user to fill in the details that they want to store will be shown.
once the user has filled in the details they need to click the "create" button

2.2 - Then the user will be shown a new window asking the user to provide their master password. This is so their website password can be encrypted, the application will use the master password as a key to access and dencrypted their password in future.

2.3 - Once done the user should see their new entry within the list of entries on the left.

### Accessing and updating stored infomation

3.1 - once logged in if the user clicks the name of the entry they want to access on the left of the screen. This will prompt the user for their master password.

3.2 - Once the master password has been entered the right side of the screen will show the details about the entry they wanted to access. from here the user can copy any of the details they need to use.

3.3 - If the user intends to update the details all they need to do is replace the text with the new information and click the "Update" button. once they have entered their mast password the information for that entry will be updated