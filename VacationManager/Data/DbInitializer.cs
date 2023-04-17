using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using VacationManager.Models;
using VacationManager.Models.AdditionalModels;
using VacationManager.Models.Enums;

namespace VacationManager.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
            _roleManager = roleManager;
        }

        public void Run(IServiceProvider serviceProvider, bool shouldDeleteDB)
        {
            if (shouldDeleteDB == true)
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();

                AddRoles();
                AddUsers(serviceProvider);
                AddProjects();
                AddTeams();
                AddVacations();
                AddUsersToTeams();

            }
        }
        public void AddRoles()
        {
            var roles = new List<string> { "Unassigned", "Developer", "Team Lead", "CEO" };
            Task<IdentityResult> roleResult;

            foreach (var role in roles)
            {
                Task<bool> hasRole = _roleManager.RoleExistsAsync(role);
                hasRole.Wait();

                if (!hasRole.Result)
                {
                    roleResult = _roleManager.CreateAsync(new IdentityRole(role));
                    roleResult.Wait();
                }
                
            }
        }
        public void AddUsers(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                //ceo
                Task<IdentityResult> ceoResult = null;
                if( ceoResult  == null)
                {
                    var ceo = new ApplicationUser()
                    {
                        UserName = "petar_ivanov",
                        Email = "petar_ivanov@gmail.com",
                        FirstName = "Petar",
                        LastName = "Ivanov"
                    };
                    ceoResult = _userManager.CreateAsync(ceo, "Petar@123");
                    ceoResult.Wait();
                    if(ceoResult.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(ceo, "CEO");
                        newUserRole.Wait();
                    }
                }


                //team leaders
                Task<IdentityResult> teamLead1Result = null;
                if (teamLead1Result == null)
                {
                    var teamLead = new ApplicationUser()
                    {
                        UserName = "ralitsa_metodieva",
                        Email = "ralitsa_metodieva@gmail.com",
                        FirstName = "Ralitsa",
                        LastName = "Metodieva"
                    };
                    teamLead1Result = _userManager.CreateAsync(teamLead, "Ralitsa@123");
                    teamLead1Result.Wait();
                    if (teamLead1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(teamLead, "Team Lead");
                        newUserRole.Wait();
                    }
                }
                Task<IdentityResult> teamLead2Result = null;
                if (teamLead2Result == null)
                {
                    var teamLead = new ApplicationUser()
                    {
                        UserName = "georgi_georgiev",
                        Email = "georgi_georgiev@gmail.com",
                        FirstName = "Georgi",
                        LastName = "Georgiev"
                    };
                    teamLead2Result = _userManager.CreateAsync(teamLead, "Georgi@123");
                    teamLead2Result.Wait();
                    if (teamLead2Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(teamLead, "Team Lead");
                        newUserRole.Wait();
                    }
                }


                Task<IdentityResult> teamLead3Result = null;
                if (teamLead3Result == null)
                {
                    var teamLead = new ApplicationUser()
                    {
                        UserName = "william_jones",
                        Email = "william_jones@gmail.com",
                        FirstName = "William",
                        LastName = "Jones"
                    };
                    teamLead3Result = _userManager.CreateAsync(teamLead, "William@123");
                    teamLead3Result.Wait();
                    if (teamLead3Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(teamLead, "Team Lead");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> teamLead4Result = null;
                if (teamLead4Result == null)
                {
                    var teamLead = new ApplicationUser()
                    {
                        UserName = "susan_richardson",
                        Email = "susan_richardson@gmail.com",
                        FirstName = "Susan",
                        LastName = "Richardson"
                    };
                    teamLead4Result = _userManager.CreateAsync(teamLead, "Susan@123");
                    teamLead4Result.Wait();
                    if (teamLead4Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(teamLead, "Team Lead");
                        newUserRole.Wait();
                    }
                }


                //developers
                Task<IdentityResult> developer1Result = null;
                if (developer1Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "thomas_hoover",
                    Email = "thomas_hoover@gmail.com",
                    FirstName = "Thomas",
                    LastName = "Hoover"
                    };
                    developer1Result = _userManager.CreateAsync(developer, "Thomas@123");
                    developer1Result.Wait();
                    if (developer1Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }
                

                Task<IdentityResult> developer2Result = null;
                if (developer2Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "marin_marinov",
                    Email = "marin_marinov@gmail.com",
                    FirstName = "Marin",
                    LastName = "Marinov"
                    };
                    developer2Result = _userManager.CreateAsync(developer, "Marin@123");
                    developer2Result.Wait();
                    if (developer2Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> developer3Result = null;
                if (developer3Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "jennifer_lewis",
                    Email = "jennifer_lewis@gmail.com",
                    FirstName = "Jennifer",
                    LastName = "Lewis"
                    };
                    developer3Result = _userManager.CreateAsync(developer, "Jennifer@123");
                    developer3Result.Wait();
                    if (developer3Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> developer4Result = null;
                if (developer4Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "james_grant",
                    Email = "james_grant@gmail.com",
                    FirstName = "James",
                    LastName = "Grant"
                    };
                    developer4Result = _userManager.CreateAsync(developer, "James@123");
                    developer4Result.Wait();
                    if (developer4Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> developer5Result = null;
                if (developer5Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "boris_hristov",
                    Email = "boris_hristov@gmail.com",
                    FirstName = "Boris",
                    LastName = "Hristov"
                    };
                    developer5Result = _userManager.CreateAsync(developer, "Boris@123");
                    developer5Result.Wait();
                    if (developer5Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> developer6Result = null;
                if (developer6Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "karina_todorova",
                    Email = "karina_todorova@gmail.com",
                    FirstName = "Karina",
                    LastName = "Todorova"
                    };
                    developer6Result = _userManager.CreateAsync(developer, "Karina@123");
                    developer6Result.Wait();
                    if (developer6Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }

                Task<IdentityResult> developer7Result = null;
                if (developer7Result == null)
                {
                    var developer = new ApplicationUser()
                    {UserName = "stoyan_popov",
                    Email = "stoyan_popov@gmail.com",
                    FirstName = "Stoyan",
                    LastName = "Popov"
                    };
                    developer7Result = _userManager.CreateAsync(developer, "Stoyan@123");
                    developer7Result.Wait();
                    if (developer7Result.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(developer, "Developer");
                        newUserRole.Wait();
                    }
                }


            }
        }
        public void AddProjects()
        {
            var project1 = new Project()
            {
                Name = "Smart Health Monitoring System",
                Description = "The Smart Health Monitoring System is an innovative healthcare solution that leverages IoT technology to improve patient care and health outcomes. The system consists of wearable health monitors and a web-based platform for real-time health data analysis. The wearable monitors will continuously track vital signs such as heart rate, blood pressure, and oxygen saturation, as well as monitor physical activity and sleep patterns. The platform will use advanced analytics and machine learning algorithms to provide actionable insights into patients' health conditions, enabling healthcare providers to make informed decisions and provide timely interventions. The system will also allow patients to monitor their own health data, empowering them to take an active role in their care. The Smart Health Monitoring System is designed to improve the quality of care, reduce healthcare costs, and enhance patient outcomes."
            };
            _context.Projects.Add(project1);

            var project2 = new Project()
            {
                Name = "Intelligent Energy Management System",
                Description = "The Intelligent Energy Management System is a smart solution that optimizes energy consumption in residential and commercial buildings. The system will use machine learning algorithms to analyze energy consumption patterns and adjust heating, cooling, and lighting systems based on occupancy and usage data. The platform will also provide real-time energy usage monitoring and analytics, enabling building managers to make informed decisions and reduce energy waste. The Intelligent Energy Management System is designed to promote energy efficiency, reduce operating costs, and reduce the carbon footprint of buildings."
            };
            _context.Projects.Add(project2);

            var project3 = new Project()
            {
                Name = "Automated Financial Analysis System",
                Description = "The Automated Financial Analysis System is an AI-powered solution that automates financial analysis and reporting processes. The system will use natural language processing and machine learning algorithms to extract financial data from various sources and generate financial reports, including balance sheets, income statements, and cash flow statements. The platform will also provide real-time financial data analytics, enabling organizations to make informed decisions and improve financial performance. The Automated Financial Analysis System is designed to streamline financial reporting processes, reduce costs, and improve data accuracy."
            };
            _context.Projects.Add(project3);

            _context.SaveChanges();
        }
        public void AddTeams()
        {
            var team1 = new Team()
            {
                ProjectId = _context.Projects.FirstOrDefault(p => p.Name == "Smart Health Monitoring System").Id,
                LeaderId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "petar_ivanov").Id,
                Name = "Code Wizards"
            };
            _context.Teams.Add(team1);

            var team2 = new Team()
            {
                ProjectId = _context.Projects.FirstOrDefault(p => p.Name == "Intelligent Energy Management System").Id,
                LeaderId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "ralitsa_metodieva").Id,
                Name = "Tech Titans"
            };
            _context.Teams.Add(team2);

            var team3 = new Team()
            {
                ProjectId = _context.Projects.FirstOrDefault(p => p.Name == "Automated Financial Analysis System").Id,
                LeaderId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "georgi_georgiev").Id,
                Name = "Code Crushers"
            };
            _context.Teams.Add(team3);

            var team4 = new Team()
            {
                ProjectId = _context.Projects.FirstOrDefault(p => p.Name == "Smart Health Monitoring System").Id,
                LeaderId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "william_jones").Id,
                Name = "Cyber Sharks"
            };
            _context.Teams.Add(team4);

            var team5 = new Team()
            {
                ProjectId = _context.Projects.FirstOrDefault(p => p.Name == "Automated Financial Analysis System").Id,
                LeaderId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "susan_richardson").Id,
                Name = "Logic Lords"
            };
            _context.Teams.Add(team5);

            _context.SaveChanges();
        }

        public void AddVacations()
        {
            var sickLeave1 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "petar_ivanov").Id,
                StartDate = DateTime.ParseExact("10.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("14.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("09.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.SickLeave,
                HalfDayVacation = false,
                Approved = true,
                FilePath = "e1321b2d-917d-45c1-98e6-23a46d62db6d_1.jpg"
            };
            _context.Vacations.Add(sickLeave1);

            var sickLeave2 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "marin_marinov").Id,
                StartDate = DateTime.ParseExact("22.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("25.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("22.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.SickLeave,
                HalfDayVacation = false,
                Approved = true,
                FilePath = "20c8a31b-c7cf-4912-8adf-934bee451312_2.jpg"
            };
            _context.Vacations.Add(sickLeave2);

            var sickLeave3 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "karina_todorova").Id,
                StartDate = DateTime.ParseExact("22.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("22.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("22.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.SickLeave,
                HalfDayVacation = true,
                Approved = true,
                FilePath = "6c9f92f5-9073-413b-bc61-b3d35ab3e804_3.jpg"
            };
            _context.Vacations.Add(sickLeave3);

            var unpaid1 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "petar_ivanov").Id,
                StartDate = DateTime.ParseExact("26.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("29.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("16.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Unpaid,
                HalfDayVacation = false,
                Approved = false,
                FilePath = null
            };
            _context.Vacations.Add(unpaid1);

            var unpaid2 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "william_jones").Id,
                StartDate = DateTime.ParseExact("01.01.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("27.01.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("20.12.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Unpaid,
                HalfDayVacation = false,
                Approved = true,
                FilePath = null
            };
            _context.Vacations.Add(unpaid2);

            var unpaid3 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "james_grant").Id,
                StartDate = DateTime.ParseExact("23.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("30.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("22.03.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Unpaid,
                HalfDayVacation = false,
                Approved = true,
                FilePath = null
            };
            _context.Vacations.Add(unpaid3);

            var unpaid4 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "karina_todorova").Id,
                StartDate = DateTime.ParseExact("06.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("15.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("01.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Unpaid,
                HalfDayVacation = false,
                Approved = true,
                FilePath = null
            };
            _context.Vacations.Add(unpaid4);

            var paid1 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "marin_marinov").Id,
                StartDate = DateTime.ParseExact("14.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("14.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("10.02.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Paid,
                HalfDayVacation = true,
                Approved = true,
                FilePath = null
            };
            _context.Vacations.Add(paid1);

            var paid2 = new Vacation()
            {
                ApplicationUserId = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "susan_richardson").Id,
                StartDate = DateTime.ParseExact("07.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("07.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequestCreationDate = DateTime.ParseExact("01.04.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                VacationOption = VacationOption.Paid,
                HalfDayVacation = true,
                Approved = true,
                FilePath = null
            };
            _context.Vacations.Add(paid2);

            _context.SaveChanges();
        }

        public void AddUsersToTeams()
        {
            var user1 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "petar_ivanov");
            user1.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Code Wizards").Id;

            var user2 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "thomas_hoover");
            user2.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Code Wizards").Id;

            var user3 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "boris_hristov");
            user3.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Code Wizards").Id;

            var user4 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "marin_marinov");
            user4.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Tech Titans").Id;

            var user5 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "ralitsa_metodieva");
            user5.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Tech Titans").Id;

            var user6 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "james_grant");
            user6.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Tech Titans").Id;

            var user7 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "jennifer_lewis");
            user7.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Code Crushers").Id;

            var user8 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "georgi_georgiev");
            user8.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Code Crushers").Id;

            var user9 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "william_jones");
            user9.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Cyber Sharks").Id;

            var user10 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "stoyan_popov");
            user10.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Cyber Sharks").Id;

            var user11 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "susan_richardson");
            user11.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Logic Lords").Id;

            var user12 = _context.ApplicationUsers.FirstOrDefault(l => l.UserName == "karina_todorova");
            user12.TeamId = _context.Teams.FirstOrDefault(t => t.Name == "Logic Lords").Id;

            _context.SaveChanges();
        }
    }
}
