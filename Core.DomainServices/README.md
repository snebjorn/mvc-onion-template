Core -> Domain Services
==================
Is the 1st ring, and can only reference the inner domain model library.

This is where you place your interfaces for working with your domain model. In other words. This is where your repository interfaces go.

These interfaces will then be implemented in the outer infrastructure layer. Like Infrastructure.DataAccess.