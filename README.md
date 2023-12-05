# Hamburger Website

The purpose of this presentation is to showcase the development process of a hamburger store website using ASP.NET MVC Core. The goals of our group project for the ASP.NET MVC hamburger website include designing and developing a user-friendly interface for customers to easily browse and place orders, creating an administrative control panel for managing orders and inventory, sending email confirmations during registration, optimizing site performance with eager loading, and providing a visually appealing and responsive website.

## Project Overview

ASP.NET MVC Core is utilized as a framework for developing dynamic and interactive web applications. By dividing the project into layers and adopting the divide-and-conquer approach, collaboration and task division among team members are facilitated. The main features of the website include user authentication, email confirmation during registration, pagination for menu management, and an order system application.

Through this project, we aim to provide customers with a seamless online experience and efficient management tools for the store. The MVC Onion Architecture in ASP.NET provides a structured approach for the website, separating the presentation layer into "Model," "View," and "Controller."

## Technologies Used

- **Entity Framework:** Used for data access, ensuring seamless integration with the database and simplifying the development process.
- **Razor and Bootstrap Framework:** Utilized to create responsive and interactive user interfaces, ensuring cross-browser compatibility.

### User Authentication

The website includes a secure user authentication system that allows customers to register and log in. Identity was used to encrypt user credentials (password hash) to ensure the privacy of user identity. Registration and authentication are fundamental features, providing users with the ability to create accounts and securely log in. Role-based authentication ensures that only authorized users can perform specific actions.

### Menu Management

The menu management system allows administrators to list, add, edit, and delete menu items and extra ingredients. Each menu item can have customizable properties such as name, description, and price. Role management, authorization, and validation techniques are implemented to ensure data integrity and prevent invalid entries.

### Mail Confirmation

A method to verify the validity of the email address provided during registration was integrated into the website.

### Pagination

Pagination was implemented to divide large amounts of data into smaller, more manageable parts, allowing users to navigate between pages.

