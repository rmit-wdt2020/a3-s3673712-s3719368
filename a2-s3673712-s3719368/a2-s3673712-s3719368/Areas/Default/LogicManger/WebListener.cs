using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.LogicManger
{
    public class WebListener : BackgroundService
    {
        private UdpClient client;
        private readonly ILogger<WebListener> _logger;
        private readonly IServiceProvider _provider;
        public IServiceProvider Services { get; }

        public WebListener(IServiceProvider services, ILogger<WebListener> logger, IServiceProvider serviceProvider)
        {
           // _context = context;
            Services = services;
            _logger = logger;
            _provider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            client = new UdpClient(888);
            
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try 
                {
                    
                    using (IServiceScope scope = _provider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<NationBankContext>();
                        List<BillPay> bills = context.BillPays.ToList();
                        List<Login> Logins = context.Logins.ToList();

                        foreach (BillPay bill in bills) //check due bills.
                        {
                            if (bill.ScheduleDate.ToString() == DateTime.UtcNow.ToString() && bill.Block == false)
                            {
                                _logger.LogInformation(
                                          "A bill is due now.");
                                await PayBill(bill, context);
                            }
                        }

                        foreach (Login login in Logins)  //check  locked logins
                        {
                            if (login.Lock == true && login.LockDate.AddMinutes(1).ToString() == DateTime.UtcNow.ToString())
                            {
                                await Unlock(login, context);
                            }
                        }


                    }
                   
                     

                }
                catch (Exception)
                {
                    await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
                 }
            }

        }

        public async Task Unlock(Login login, NationBankContext _context) {
            login.Lock = false;
            login.attempt = 0;
            await _context.SaveChangesAsync();
        }

        public async Task PayBill(BillPay bill, NationBankContext _context) 
        {
            Account account = await _context.Accounts.FindAsync(bill.AccountNumber);
            if (account.PayBill(bill.Amount))
            {
                //bill paid
            }
            else 
            {
                bill.Block = true; //if not enough balance, block the bill
            }
            // Period cases
            switch(bill.Period)
            {
                case Period.Monthly:
                    bill.ScheduleDate = bill.ScheduleDate.AddMonths(1);
                    _context.Update(bill);
                    break;
                case Period.Annually:
                    bill.ScheduleDate = bill.ScheduleDate.AddYears(1);
                    _context.Update(bill);
                    break;
                case Period.Quarterly:
                    bill.ScheduleDate = bill.ScheduleDate.AddMonths(3);
                    _context.Update(bill);
                    break;
                case Period.Once_off:
                    _context.Remove(bill);
                    break;
            }
            await _context.SaveChangesAsync();

        }


        public override void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
            base.Dispose();
        }
    }
}
