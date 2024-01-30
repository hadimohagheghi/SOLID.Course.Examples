> ## اصول SOLID

**پشت پرده برنامه های ناموفق**  
برنامه نویسها در حین کدنویسی معمولا سعی می¬کنند کد های تمیزی بر اساس دانش و تجربه خودشان بنویسند، اما خیلی وقتها برنامه ها به باگ میخورند. بعد از مدتی هر برنامه ای میخواهد توسعه پیدا کند یا به هر دلیلی نیاز به تغییر دارد، از این رو نمیتوانیم جلوی توسعه را بگیریم  
نقص های زیر میتوانند باعث خراب شدن عملکرد یک نرم افزار شوند  
1- ایجاد استرس بالا روی یک کلاس با محول کردن وظایف خیلی زیاد  
2- ایجاد وابستگی های زیاد بین کلاس ها به طوری تغییر هر کدام از آنها می¬تواند روی بقیه تاثیر بگذارد  
3- پخش کردن کدهای تکراری در جای جای برنامه  
راه حل ها:  
1- استفاده از معماری صحیح  
2- پیروی از اصول طراحی  
3- استفاده از الگوهای طراحی و نوشتن نرم افزار بر اساس آنها  
اصول طراحی solid به ما این امکان را میدهد که از یک سری کد tightly coupled و little encapsulation به سمت کدهای loosely coupled و کپسوله شده بر اساس نیاز برویم.

> ### اصل تک وظیفه single responsibility principle

نکته : Srp میگه " هریک از ماژول های نرم افزار تنها باید یک دلیل برای تغییر داشته باشند".  
این بدین معنی است که هر کلاس یا ساختاری تنها باید یک کار برای انجام داشته باشد. هرچیزی در این کلاس باید وابسته به یک "هدف" باشد.  
کلاس ما نباید مثل چاقوی تصویر بالا برای هرکاری خودش را تغییر دهد. البته این بدین معنی نیست که کلاس های شما باید شامل یک متد یا یک property باشند.  
امکان دارد عضوهای زیادی وجود داشته باشند که فقط یک وظیفه دارند  
برای مثال در کد زیر این موضوع رعایت نشده است

```csharp
   public class UserService  
   {  
      public void Register(string email, string password)  
      {  
         if (!ValidateEmail(email))  
            throw new ValidationException("Email is not an email");  
            var user = new User(email, password);  
     
            SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject="HEllo foo" });  
      }
      public virtual bool ValidateEmail(string email)  
      {  
        return email.Contains("@");  
      }  
      public bool SendEmail(MailMessage message)  
      {  
        _smtpClient.Send(message);  
      }  
   }
```

همانطور که دیده می شود، در این کد user service هم ثبت نام می کند هم ایمیل میفرستد، برای رعایت srp باید کلاس مربوط ثبت نام از ایمیل زدن جدا شود

```csharp
   public class UserService  
   {  
      EmailService _emailService;  
      DbContext _dbContext;  
      public UserService(EmailService aEmailService, DbContext aDbContext)  
      {  
         _emailService = aEmailService;  
         _dbContext = aDbContext;  
      }  
      public void Register(string email, string password)  
      {  
         if (!_emailService.ValidateEmail(email))  
            throw new ValidationException("Email is not an email");  
            var user = new User(email, password);  
            _dbContext.Save(user);  
            emailService.SendEmail(new MailMessage("myname@mydomain.com", email) {Subject="Hi. How are you!"});  
     
         }  
      }
     
   public class EmailService  
   {  
         SmtpClient _smtpClient;  
      public EmailService(SmtpClient aSmtpClient)  
      {  
         _smtpClient = aSmtpClient;  
      }  
      public bool virtual ValidateEmail(string email)  
      {  
         return email.Contains("@");  
      }  
      public bool SendEmail(MailMessage message)  
      {  
         _smtpClient.Send(message);  
      }  
   }
```

همانطور که دیده می شود در مثال بالا یک کلاس مسئول ثبت نام و کلاس دیگری مسئول ارسال ایمیل است.

هدف:  
این اصل تلاش می کند که رفتار ها را از هم جدا کند. به این منظور که اگر در اثر تغییر باگی به وجود آمد رفتارهای غیر مرتبط تحت تاثیر قرار نگیرند.

> ### اصل باز و بسته (Open/Closed Principle)

این اصل می گوید که " یک ماژول یا کلاس باید برای گسترش باز و برای تغییر بسته باشد"  
"باز بودن برای گسترش" به این معنی است که، ما نیاز به طراحی¬ای داریم که تنها در زمانی عملکرد جدید اضافه می شود که نیاز جدیدی به وجود آمده باشد.  
"بسته بودن برای تغییر" به این معنی است که اگر یک کلاس را توسعه دادیم و از تست واحد عبور کرد، تغییر نمی کند مگر اینکه به باگ برخورد کند.

در مثال زیر فرض کنید یک کلاس داریم که بیانگر یک مستطیل است و کلاسی داریم که وظیفه محاسبه مساحت مجموعه ای از مستطیل¬ها را دارد. در این حالت srp هم به طور کامل رعایت شده است.

```csharp
public class Rectangle
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }

public class AreaCalculator
    {
        public double TotalArea(Rectangle[] arrRectangles)
        {
            double area=0;
            foreach (var objRectangle in arrRectangles)
            {
                area += objRectangle.Height * objRectangle.Width;
            }
            return area;
        }
   {
```

در حال حاضر این app هیچ ایرادی ندارد، حال اگر بخواهیم مساحت دایره هم حساب کنیم مجبوریم متد Total Area را تغییر دهیم و در آن چک کنیم که اگر دایره بود چکار کن، اگر مستطیل بود چکار. همین امر باعث زیر سوال بردن بسته بودن نسبت به تغییر می شود.  
مجبوریم متدهای خود را به شکل زیر تغییر دهیم:

```csharp
     public double TotalArea(object[] arrObjects)
        {
            double area = 0;
            Rectangle objRectangle = new Rectangle()  ;
            Circle objCircle;
            foreach (var obj in arrObjects)
            {
                if (obj is Rectangle)
                {
                    area += objRectangle.Height * objRectangle.Width;
                }
                else
                {
                    objCircle = (Circle)obj;
                    area += objCircle.Radius * objCircle.Radius * Math.PI;
                }
            }
            return area;
        }
```

اوکی. حال فرض کنیم میخواهیم شی مثلث هم اضافه کنیم، همواره باید این متد را تغییر دهیم.  
به طور عمومی راهکار این است که وابسته ها را از سطح کلاس به اینترفیس و کلاسهای abstract تغییر دهیم.

```csharp
  public abstract class Shape
   {
        public abstract double Area();
   {
```

در این حالت متد محاسبه مساحت به صورت abstract آمده است و هر کدام از کلاس های زیر باید آن را پیاده سازی کنند.

```csharp
public class Circle : Shape
    {
        public double Radius { get; set; }
        public override double Area()
        {
            return Radius * Radius * Math.PI;
        }
    }
public class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public override double Area()
        {
            return Height * Width;
        }
    }
  public class AreaCalculator
    {
        public double TotalArea(Shape[] arrShapes)
        {
            double area = 0;
            foreach (var objShape in arrShapes)
            {
                area += objShape.Area();
            }
            return area;
        }
  {
```

> ### اصل جایگزینی لیسکوف Liskov Substitution Principle

اصل جایگزینی لیسکوف می گوید شما باید بتوانید از کلاس های فرزند به جای پدر بدون هیچ تغییری استفاده نمایید. به بیانی دیگر به این نکته تاکید دارد که کلاس فرزند نباید روی رفتارهای پدر تاثیر بگذارد.  
این اصل فقط یک اکستنشن از اصل باز و بسته است به این معنی که کلاس فرزند فقط باید کلاس پدر را extend کند، و رفتار آن را تغییر ندهد.  
برای مثال اگر پدر دکتر باشد و پسر آن فوتبالیست بشود، پسر نمی تواند جایگزین پدر شود، پس اصل جایگزینی لیسکوف نقض می¬گردد.

مثال:  
در این مثال، ابتدا میخواهیم ببینیم چگونه اصل جایگزینی لیسکوف زیر سوال می رود، سپس در ادامه آن را اصلاح نماییم. هدف این است که یک app داشته باشیم که داده¬ها را توسط گروهی از فایلهای متنی sql مدیریت کنیم. در اینجا نیاز داریم عملکردی را برقرار کنیم که در آن داده¬ها قابلیت لود و ذخیره شدن داشته باشند. بنابراین نیاز به کلاسی داریم که این کار را انجام دهد.

```csharp

    public class SqlFile  
    {  
       public string FilePath {get;set;}  
       public string FileText {get;set;}  
       public string LoadText()  
       {  
          /* Code to read text from sql file */  
       }  
       public string SaveText()  
       {  
          /* Code to save text into sql file */  
       }  
    }  
    public class SqlFileManager  
    {  
       public List<SqlFile> lstSqlFiles {get;set}  
      
       public string GetTextFromFiles()  
       {  
          StringBuilder objStrBuilder = new StringBuilder();  
          foreach(var objFile in lstSqlFiles)  
          {  
             objStrBuilder.Append(objFile.LoadText());  
          }  
          return objStrBuilder.ToString();  
       }  
       public void SaveTextIntoFiles()  
       {  
          foreach(var objFile in lstSqlFiles)  
          {  
             objFile.SaveText();  
          }  
       }  
    }
```

تا اینجا همه چیز اوکیه. پس از مدتی تصمیمی اتخاذ میگردد مبنی بر اینکه تعدادی فایل فقط خواندنی باشند، پس ما مجبوریم که آنها را محدود کنیم  
می توانیم کلاسی تحت عنوان ReadOnlySqlFile بسازیم که از SqlFile ارث بری کند، و متد saveTextIntoFiles را تغییر دهد که از ذخیره شدن در فایل های readonly جلوگیری کند.

```csharp

    public class SqlFile  
    {  
       public string LoadText()  
       {  
       /* Code to read text from sql file */  
       }  
       public void SaveText()  
       {  
          /* Code to save text into sql file */  
       }  
    }  
    public class ReadOnlySqlFile: SqlFile  
    {  
       public string FilePath {get;set;}  
       public string FileText {get;set;}  
       public string LoadText()  
       {  
          /* Code to read text from sql file */  
       }  
       public void SaveText()  
       {  
          /* Throw an exception when app flow tries to do save. */  
          throw new IOException("Can't Save");  
       }  
    }
```

بنابراین باید کلاس SqlFileManager را با اضافه کردن چند شرط تغییر دهیم.

```csharp
    public class SqlFileManager  
    {  
       public List<SqlFile? lstSqlFiles {get;set}  
       public string GetTextFromFiles()  
       {  
          StringBuilder objStrBuilder = new StringBuilder();  
          foreach(var objFile in lstSqlFiles)  
          {  
             objStrBuilder.Append(objFile.LoadText());  
          }  
          return objStrBuilder.ToString();  
       }  
       public void SaveTextIntoFiles()  
       {  
          foreach(var objFile in lstSqlFiles)  
          {  
             //Check whether the current file object is read-only or not.If yes, skip calling it's  
             // SaveText() method to skip the exception.  
      
             if(! objFile is ReadOnlySqlFile)  
             objFile.SaveText();  
          }  
       }  
    }
```

در اینجا ما متد SaveTextIntoFiles را در SqlFileManager تغییر دادیم که مشخص میکند، آیا نمونه ای ReadOnlySqlFile هست یا نه .  
در اینجا ما نمیتوانیم از ReadOnlySqlFile بدون تغییر SqlFileManager به جای پدرش استفاده کنیم. پس می¬توانیم بگوییم این طراحی از LSP پیروی نمی¬کند. برای رفع این مشکل به روشی که در ادامه توضیح داده می¬شود عمل می کنیم.  
ابتدا دو اینترفیس ایجاد می¬کنیم.

```csharp
    public interface IReadableSqlFile  
    {  
       string LoadText();  
    }  
    public interface IWritableSqlFile  
    {  
       void SaveText();  
    }
```

حال این اینترفیس را پیاده سازی می¬کنیم.

```csharp
    public class ReadOnlySqlFile: IReadableSqlFile  
    {  
       public string FilePath {get;set;}  
       public string FileText {get;set;}  
       public string LoadText()  
       {  
          /* Code to read text from sql file */  
       }  
    }
    public class SqlFile: IWritableSqlFile,IReadableSqlFile  
    {  
       public string FilePath {get;set;}  
       public string FileText {get;set;}  
       public string LoadText()  
       {  
          /* Code to read text from sql file */  
       }  
       public void SaveText()  
       {  
          /* Code to save text into sql file */  
       }  
    }
```

حال طراحی کلاس SqlFileManager به صورت زیر می¬شود

```csharp
    public class SqlFileManager  
    {  
       public string GetTextFromFiles(List<IReadableSqlFile> aLstReadableFiles)  
       {  
          StringBuilder objStrBuilder = new StringBuilder();  
          foreach(var objFile in aLstReadableFiles)  
          {  
             objStrBuilder.Append(objFile.LoadText());  
          }  
          return objStrBuilder.ToString();  
       }  
       public void SaveTextIntoFiles(List<IWritableSqlFile> aLstWritableFiles)  
       {  
       foreach(var objFile in aLstWritableFiles)  
       {  
          objFile.SaveText();  
       }  
```

در اینجا متد GetTextFromFiles() فقط لیستی از نمونه هایی را می گیرد که اینترفیس IReadOnlySqlFile را پیاده سازی کرده باشند. و همچین متد SaveTextInrtoFiles فقط لیستی از می¬گیرد که اینترفیس IWritableSqlFile را پیاده سازی کرده باشند. به بیانی دیگر، در این مثال SqlFile ها.

> اصل جداسازی اینترفیس ها Interface Segregation Principle (ISP)

این اصل بیان میکند که کلاینت¬ها نباید مجبور به پیاده سازی اینترفیسهایی باشند که آنها نیازی ندارند. به جای یک اینترفیس چاق، چندین اینترفیس کوچک داشته باشیم که گروهی از متدها درون خود داشته باشند.  
این میتواند به این معنی باشد که اینترفیس¬ها باید در وهله¬ی اول از SRP پیروی کنند.  
فرض کنید میخواهیم یک سیستم داشته باشیم که در آن برای یک شرکت IT که شامل نقشهای TeamLead و Programmer است که در آن TeamLead تسک های بزرگ را به تسک¬های کوچک تبدیل می کند، و آن ها را به برنامه نویس¬های خودش assign میکند.

```csharp
    public Interface ILead  
    {  
       void CreateSubTask();  
       void AssginTask();  
       void WorkOnTask();  
    }  
    public class TeamLead : ILead  
    {  
       public void AssignTask()  
       {  
          //Code to assign a task.  
       }  
       public void CreateSubTask()  
       {  
          //Code to create a sub task  
       }  
       public void WorkOnTask()  
       {  
          //Code to implement perform assigned task.  
       }  
    }
```

تا اینجا همه چیز خوب است. حال فرض کنید یک نقش به عنوان Manager وارد تیم می شود، ولی فقط می خواهد به TeamLead تسک assign کند، و قرار نیست روی تسکی کار کند. کلاس Manager اینترفیس ILead را به صورت زیر پیاده سازی می کند، به صورتی که نتواند روی تسک کار کند.

```csharp
    public class Manager: ILead  
    {  
       public void AssignTask()  
       {  
          //Code to assign a task.  
       }  
       public void CreateSubTask()  
       {  
          //Code to create a sub task.  
       }  
       public void WorkOnTask()  
       {  
          throw new Exception("Manager can't work on Task");  
       }  
    }
```

همانطور که دیده می¬شود کلاس Manager مجبور به پیاده سازی متدی شد که اصلا نیازی به آن ندارد. و این اصل ISP را زیر سوال می برد . برای رفع این موضوع ، یک اینترفیس IProgrammer می¬سازیم که تنها امضای متد WorkOnTask در آن آمده است، و یک اینترفیس ILead می سازیم که در آن AssignTask, CreateSubTask آمده است.

```csharp
    public interface IProgrammer  
    {  
       void WorkOnTask();  
    }  

    public interface ILead  
    {  
       void AssignTask();  
       void CreateSubTask();  
    } 

    public class Programmer: IProgrammer  
    {  
       public void WorkOnTask()  
       {  
          //code to implement to work on the Task.  
       }  
    }  
    public class Manager: ILead  
    {  
       public void AssignTask()  
       {  
          //Code to assign a Task  
       }  
       public void CreateSubTask()  
       {  
       //Code to create a sub taks from a task.  
       }  
    }
```

همانطور که دیده می شود دیگر manager مجبور به پیاده سازی متد اضافی نیست.حال کلاس TeamLead باید از هردو اینترفیس ILead,IProgrammer پیروی کند.

```csharp
    public class TeamLead: IProgrammer, ILead  
    {  
       public void AssignTask()  
       {  
          //Code to assign a Task  
       }  
       public void CreateSubTask()  
       {  
          //Code to create a sub task from a task.  
       }  
       public void WorkOnTask()  
       {  
          //code to implement to work on the Task.  
       }  
    } 
```

در اینجا دیده می شود که هدف و مسئولیت¬ها به خوبی از هم جدا شده اند.
