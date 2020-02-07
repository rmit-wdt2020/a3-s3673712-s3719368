# a3-s3673712-s3719368 - ASP.NET Core Internet Banking website
## Contributors

Bach Truong Dao - s3673712	Yongqian Huang - s3719268

## Structure
Project 1: a2-s3673712-s371938 - Admin Portal build upon assignment 2 (client side) 
Area pattern: 
+ Default area -  use for customer login and atm function
+ Admin area - use for admin portal and controlling back-end database  
Use program.cs to run website : 
+ /Bank/SecureLogin to login with client
+ /Bank/SecureAdminLogin to login with admin: Username : admin, Password: admin

Project 2: WebApi - ASP.NET Core Web API that will provide the admin the access
to the database and the admin portal features

Logics are separated by using Manager files

If account is block after 3 incorrect log in, it will be unlock after 1 min

```C#
 if (login.Lock == true && login.LockDate.AddMinutes(1).ToString() == DateTime.UtcNow.ToString())
                            {
                                await Unlock(login, context);
                            }
```
Unlock function:

```C#
     public async Task Unlock(Login login, NationBankContext _context) {
            login.Lock = false;
            login.attempt = 0;
            await _context.SaveChangesAsync();
        }
```
To Block Schedule Pay, using toogle switch
```C#
         var billpay = await billmanager.GetBillPay(id);
            if(billpay == null)
                return NotFound();
            billmanager.Block(billpay, !billpay.Block); //Change to true(Block) or false(Unblock) when switch is toogled
```
To Lock an Login, using button Lock/Unlock. A checkbox will be shown in the view
```C#
        var login = await loginManger.GetLogin(CustomerID);
            if (login == null)
                return NotFound();
            loginManger.Lock(login, !login.Lock); //Change to true(Lock) or false(Unlock) when button clicked
                return View(login);
```
## References
+ Codes are copied and modified from lecture examples/ lab examples from weeks 4-9 
+ Css stylesheet from [fontawesome-icon](https://fontawesome.com/)
+ Images copied and modified from internet:
[Error.png](https://images.template.net/wp-content/uploads/2016/09/30143726/Creative-404-Page-Designs.jpg) and
[Login_background](https://images.unsplash.com/photo-1501167786227-4cba60f6d58f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80)
and RMIT Logo
+ Stackoverflow 
