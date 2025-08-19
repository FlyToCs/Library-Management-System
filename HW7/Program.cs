using Figgle.Fonts;
using HW6.Extentions;
using HW7.Contract;
using HW7.Database;
using HW7.Entities;
using HW7.Services;
using PersianConsole;
using Sharprompt;
using Spectre.Console;

IAuthentication authentication = new Authentication();
IMemberService memberService = new MemberService();
ILibrarianService librarianService = new LibrarianService();

Console.OutputEncoding = System.Text.Encoding.UTF8;


// ConvertConsole.Enable(); 
// string text = "مثلا الکی داره لود میشه!";
// Console.WriteLine(ConvertConsole.ConvertString(text)); 
// ConvertConsole.Disable();

Console.WriteLine("نسخه اول کتابخانه".Reverse().ToArray());

await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        // Define tasks
        var task1 = ctx.AddTask("[red]Connecting to database[/]");
        var task2 = ctx.AddTask("[aqua]Loading application[/]");

        while (!ctx.IsFinished)
        {
            // Simulate some work
            await Task.Delay(150);

            // Increment
            task1.Increment(5.5);
            task2.Increment(3.0);
        }
    });




Authentication(true);




void Authentication(bool flag)
{
    while (flag)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Authentication"));
        Console.ResetColor();

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Login",
            "2. Register",
            "3. Exit"
        });
        Console.WriteLine("------------------");



        switch (select)
        {
            case "1. Login":
                Console.Write("\nEmail: ");
                string email = Console.ReadLine()!;
                if (!StringValidation(email)) break;
                    

                Console.Write("Password: ");
                string password = Console.ReadLine()!;
                if (!StringValidation(password)) break;

                Storage.CurrentUser = authentication.Login(email, password);
                
                if (Storage.CurrentUser is not null)
                {
                    ConsolePainter.GreenMessage("login successfully");
                    Console.ReadKey();
                    MainMenu(Storage.CurrentUser.Role);
                }
                    
                else
                    ConsolePainter.RedMessage("the username or password is not correct");
                
                Console.ReadKey();
                break;
            case "2. Register":

                Console.Write("\nEmail: ");
                string newEmail = Console.ReadLine()!;
                if (!StringValidation(newEmail)) break;


                Console.Write("Password: ");
                string newPassword = Console.ReadLine()!;
                if (!StringValidation(newPassword)) break;

                RoleEnum newRole;
                var role = Prompt.Select("Select a Role", new[] { "Member", "Librarian"});
                if (role == "Member") newRole = RoleEnum.Member;
                else newRole = RoleEnum.Librarian;

                if (newRole == RoleEnum.Member) 
                {
                    Storage.CurrentUser = new Member(null!, null!, newEmail, newPassword, newRole ,null!);
                }
                else
                {
                    Storage.CurrentUser = new Librarian(null!, null!, newEmail, newPassword, newRole);
                }
                Storage.userList.Add(Storage.CurrentUser);

                ConsolePainter.GreenMessage("Register successfully");
                Console.ReadKey();
                MainMenu(Storage.CurrentUser.Role);
                break;
            case "3. Exit":
                flag = false;
                break;
        }
    }
}



void MainMenu(RoleEnum role)
{
    Console.Clear();
    if (Storage.CurrentUser != null && (Storage.CurrentUser.FirstName == null || Storage.CurrentUser.LastName == null))
    {
        while (Storage.CurrentUser.FirstName == null)
        {
            Console.Clear();
            Console.WriteLine("Please complete your profile\n");
            Console.Write("Firstname: ");
            string firstName = Console.ReadLine()!;
            if (StringValidation(firstName))
                Storage.CurrentUser.FirstName = firstName;

        }

        while (Storage.CurrentUser.LastName == null)
        {
            Console.Clear();
            Console.WriteLine("Please complete your profile\n");
            Console.Write("Lastname: ");
            string lastName = Console.ReadLine()!;

            if (StringValidation(lastName))
                Storage.CurrentUser.LastName = lastName;
        }
    }

    if (role == RoleEnum.Member)
    {
        Member currentMember = (Member)Storage.CurrentUser!;
        while (true)
        {
            Console.Clear();
            ConsolePainter.RedMessage("══════════════════════════════════════════════════════════════════");
            ConsolePainter.YellowMessage(FiggleFonts.Standard.Render("Member Panel"));
            ConsolePainter.RedMessage("══════════════════════════════════════════════════════════════════\n");

            var select = Prompt.Select("Select an option", new[]
            {
                "1. Borrow a book",
                "2. Return a Book",
                "3. View My Borrowed Books",
                "4. View All Available Books in the Library",
                "5. Logout"
            });

            #region ColorfullSeleetion
            // var select = AnsiConsole.Prompt(
            //     new SelectionPrompt<string>()
            //         .Title("[yellow]Select an option[/]")
            //         .HighlightStyle(new Style(foreground: Color.Red)) // Highlight color
            //         .MoreChoicesText("[grey](Use arrow keys to navigate)[/]")
            //         .AddChoices(new[]
            //         {
            //             "[cyan]1. Borrow a book[/]",
            //             "[cyan]2. Return a Book[/]",
            //             "[cyan]3. View My Borrowed Books[/]",
            //             "[cyan]4. View All Available Books in the Library[/]",
            //             "[cyan]5. Logout[/]"
            //         }));


            #endregion

            Console.WriteLine("------------------");

            switch (select)
            {
                case "1. Borrow a book":
                    ConsolePainter.WriteTable(memberService.GetListOfLibraryBooks(), ConsoleColor.Yellow,
                        ConsoleColor.Cyan);
                    Console.Write("\nEnter a book id: ");
                    int bookId = int.Parse(Console.ReadLine()!);
                    
                    if (memberService.BorrowBook(currentMember, memberService.SearchBook(bookId)))
                    {
                        ConsolePainter.GreenMessage("You borrowed this book");
                    }
                    else
                    {
                        ConsolePainter.RedMessage("Failed");
                    }

                    Console.ReadKey();
                    break;

                case "2. Return a Book":
                    ConsolePainter.WriteTable(memberService.GetListOfUserBooks(currentMember), ConsoleColor.Yellow,
                        ConsoleColor.Cyan);

                    Console.Write("\nEnter an id of a book to return: ");
                    string returnBookId = Console.ReadLine()!;


                    if (String.IsNullOrEmpty(returnBookId))
                    {
                        ConsolePainter.RedMessage("Failed");
                        Console.ReadKey();
                        break;
                    }

                    if (memberService.ReturnBook(currentMember, memberService.SearchBook(int.Parse(returnBookId))))
                    {
                        ConsolePainter.GreenMessage("the book has returned");
                    }
                    else
                    {
                        ConsolePainter.RedMessage("Failed");
                    }

                    Console.ReadKey();
                    break;

                case "3. View My Borrowed Books":
                    ConsolePainter.WriteTable(memberService.GetListOfUserBooks(currentMember), ConsoleColor.Yellow,
                        ConsoleColor.Cyan);
                    Console.ReadKey();
                    break;

                case "4. View All Available Books in the Library":
                    ConsolePainter.WriteTable(memberService.GetListOfLibraryBooks(), ConsoleColor.Yellow,
                        ConsoleColor.Cyan);
                    Console.ReadKey();
                    break;

                case "5. Logout":
                    Authentication(true);
                    break;
            }
        }


    }

    else if (role == RoleEnum.Librarian)
    {
        Librarian currentLibrarian = (Librarian)Storage.CurrentUser!;
        while (true)
        {
            Console.Clear();
            Console.Clear();
            ConsolePainter.CyanMessage("══════════════════════════════════════════════════════════════════════");
            ConsolePainter.YellowMessage(FiggleFonts.Standard.Render("Librarian Panel"));
            ConsolePainter.CyanMessage("══════════════════════════════════════════════════════════════════════\n");
            var select = Prompt.Select("Select an option", new[]
            {
                "1. User List",
                "2. Available book",
                "3. Details of a Member",
                "4. View All Available Books in the Library",
                "5. Logout"
            });
            Console.WriteLine("------------------");

            switch (select)
            {
                case "1. User List":
                    ConsolePainter.WriteTable(librarianService.GetListOfUsers(), ConsoleColor.Yellow, ConsoleColor.Cyan);
                    Console.ReadKey();
                    break;

                case "2. Available book":
                    ConsolePainter.WriteTable(librarianService.GetListOfLibraryBooks(), ConsoleColor.Yellow, ConsoleColor.Cyan);
                    Console.ReadKey();
                    break;

                case "3. Details of a Member":

                    ConsolePainter.YellowMessage(new string("این بخش در اپدیت بعدی به برنامه اضافه خواهد شد".Reverse().ToArray()));
                    Console.ReadKey();
                    break;

                case "4. View All Available Books in the Library":
                    ConsolePainter.YellowMessage(new string("این بخش در اپدیت بعدی به برنامه اضافه خواهد شد".Reverse().ToArray()));
                    Console.ReadKey();
                    break;

                case "5. Logout":
                    Authentication(true);
                    break;
            }
        }
    }
}






bool StringValidation(string str)
{
    if (String.IsNullOrEmpty(str))
    {
        ConsolePainter.RedMessage("The input can not ve null or empty");
        Console.ReadKey();
        return false;
    }

    if (String.IsNullOrWhiteSpace(str))
    {
        ConsolePainter.RedMessage("The input can not ve null or whit space");
        Console.ReadKey();
        return false;
    }

    if (str.Length <3)
    {
        ConsolePainter.RedMessage("The input must have 3 character at least");
        Console.ReadKey();
        return false;
    }
    return true;
}


