# Day 07 – Bootcamp .NET
### Reflection

# Contents
1. [Chapter I](#chapter-i) \
	[General Rules](#general-rules)
2. [Chapter II](#chapter-ii) \
	[Rules of the Day](#rules-of-the-day)
3. [Chapter III](#chapter-iii) \
	[Intro](#intro)
4. [Chapter IV](#chapter-iv) \
	[Exercise 00 – Code Documentation](#exercise-00-code-documentation)
5. [Chapter V](#chapter-v) \
  [Exercise 01 – Breaking the rules](#exercise-01-breaking-the-rules) 
6. [Chapter VI](#chapter-vi) \
  [Exercise 02 – Autofill](#exercise-02-autofill) 
7. [Chapter VII](#chapter-vii) \
  [Exercise 03 – Creating objects](#exercise-03-creating-objects)
8. [Bonus task](#bonus-task)

# Chapter I 

## General Rules
- Make sure you have [the .NET 5 SDK](<https://dotnet.microsoft.com/download>) installed on your computer and use it.
- Remember, your code will be read! Pay special attention to the design of your code and the naming of variables. Adhere to commonly accepted [C# Coding Conventions](<https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions>).
- Choose your own IDE that is convenient for you.
- The program must be able to run from the dotnet command line.
- Each of the exercise contains examples of input and output. The solution should use them as the correct format.
- At the beginning of each task, there is a list of allowed language constructs.
- If you find the problem difficult to solve, ask questions to other piscine participants, the Internet, Google or go to StackOverflow.
- You may see the main features of C# language in [official specification](<https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/introduction>).
- Avoid **hard coding** and **"magic numbers"**.
- You demonstrate the complete solution, the correct result of the program is just one of the ways to check its correct operation. Therefore, when it is necessary to obtain a certain output as a result of the work of your programs, it is forbidden to show a pre-calculated result.
- Pay special attention to the terms highlighted in **bold** font: their study will be useful to you both in performing the current task, and in your future career of a .NET developer.
- Have fun :)


# Chapter II
##  Rules of the Day

- Each of the task must correspond to a separate console application created based on a standard template .NET SDK.
- Use **var**.
- The name of the solution (and its separate catalog) is d {xx}, where xx are the digits of the current day. The names of the projects are specified in the exercise.
- To format the output data, use the en-US culture: N2 for the output of monetary amounts, d for dates.

## What's new

- Reflection
- Metaprogramming
- Attributes
- Runtime
- Open-source software
- Factory

## Allowed language constructs

- Activator
- params, typeof
- Type
- Attribute

# Chapter III
## Intro

“Since everything is a reflection of our minds, everything can be changed by our minds.”

**― Gautama Buddha**

You have already mastered C# so much that you decided to reflect a little. But no, it's not about reflecting on your mental state or experiences. You have decided to discover the dark side of .NET - Reflection. Reflection is a type of metaprogramming that allows you to explore a program in **runtime**. For example, get information about the types, composition and properties of fields. And even slightly modify their behavior.

We live in an amazing era when there is a lot of information in the public domain around us. It also applies to software. There is a wonderful film about the history of the development of the **open-source software movement** - [Revolution OS](https://en.wikipedia.org/wiki/Revolution_OS). Watch it at your leisure.

In the meantime, we’re glad that Microsoft has also joined this movement and the entire source code of the platform .NET is available on [github](https://github.com/dotnet). And also, for any library, which we use in everyday development, we can not only study the source code on the github, but also get information about it right in the code while our application is running.

Let's take a look at some of them.

Note. For a better understanding of the exercises, it is better to do them in the specified order.


# Chapter IV
## Exercise 00 – Code Documentation

### Project structure:
```
d07_ex00\
      Program.cs
```

### The objective

Well-documented code is the code which is much easier to maintain. Everyone knows it. And they also know, it is sometimes forgotten or there is not enough time for good documentation.

What if you can try to do this automatically?

There are already a lot of ideas and implementations for this, but here and now let's look at the basics. Let's take a real-life [DefaultHttpContext](<https://github.com/dotnet/aspnetcore/blob/c660e9b1e3d6918e327499d340cbc38065e34436/src/Http/Http/src/DefaultHttpContext.cs>) class. It is contained in the Microsoft.AspNetCore.Http nuget package and is actively used in ASP.NET Core to store and manage information about http requests and responses.

Reflection will help us get information about the types, properties, methods of this class. Why not use it and display a description of all of them on the screen? The **Type class**, which is essentially the root of **System.Reflection** and the main way to access **metadata**, and its **methods** will help us. An object of the Type class for the DefaultHttpContext can be obtained using **typeof**.

Let's start with general information: let's output the full name of the type, the description of its **Assembly** and the name of its base type.

Next, we will get a description of its [fields](<https://docs.microsoft.com/ru-ru/dotnet/api/system.type.getfields?view=net-5.0>) for the DefaultHttpContext class - let's output the name and type name for each field in the FieldInfo list. We will do the same for the [properties](<https://docs.microsoft.com/ru-ru/dotnet/api/system.type.getproperties?view=net-5.0>) and the PropertyInfo list.
Think about [how they differ](<https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties>).

DefaultHttpContext probably also has [methods](<https://docs.microsoft.com/en-us/dotnet/api/system.type.getmethods?view=net-5.0>). Their list - the list of MethodInfo objects - also needs to be output, but for each of them, specify the name, the type returned by the method, and the method parameters.

When getting these lists, pay attention to **BindingFlags**. In the output, we are interested in public and non-public fields and only public methods and properties.

You need to take into account both **static** and non-static ones.
None of the lists above requires sorting at the  output. You don't write code in alphabetical order, do you?

### Output format

```
Type: {FullName}
Assembly: {AssemblyFullName}
Based on: {BaseType}

Fields: 
{field.TypeName} {field.Name}
...

Properties:
{property.TypeName} {property.Name}
...

Methods:
{method.ReturnTypeName} {method.Name} ({method.Parameters[]})
...
```

### Example of launching an application from the project folder

```
$ dotnet run
Type: Microsoft.AspNetCore.Http.DefaultHttpContext
Assembly: Microsoft.AspNetCore.Http, Version=2.2.2.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
Based on: Microsoft.AspNetCore.Http.HttpContext

Fields:
Microsoft.AspNetCore.Http.Features.FeatureReferences`1[Microsoft.AspNetCore.Http.DefaultHttpContext+FeatureInterfaces] _features
Microsoft.AspNetCore.Http.HttpRequest _request
Microsoft.AspNetCore.Http.HttpResponse _response
Microsoft.AspNetCore.Http.Authentication.AuthenticationManager _authenticationManager
Microsoft.AspNetCore.Http.ConnectionInfo _connection
Microsoft.AspNetCore.Http.WebSocketManager _websockets
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.IItemsFeature] _newItemsFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.IServiceProvidersFeature] _newServiceProvidersFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.Authentication.IHttpAuthenticationFeature] _newHttpAuthenticationFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.IHttpRequestLifetimeFeature] _newHttpRequestLifetimeFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.ISessionFeature] _newSessionFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.ISessionFeature] _nullSessionFeature
System.Func`2[Microsoft.AspNetCore.Http.Features.IFeatureCollection,Microsoft.AspNetCore.Http.Features.IHttpRequestIdentifierFeature] _newHttpRequestIdentifierFeature

Properties:
Microsoft.AspNetCore.Http.Features.IFeatureCollection Features
Microsoft.AspNetCore.Http.HttpRequest Request
Microsoft.AspNetCore.Http.HttpResponse Response
Microsoft.AspNetCore.Http.ConnectionInfo Connection
Microsoft.AspNetCore.Http.Authentication.AuthenticationManager Authentication
Microsoft.AspNetCore.Http.WebSocketManager WebSockets
System.Security.Claims.ClaimsPrincipal User
System.Collections.Generic.IDictionary`2[System.Object,System.Object] Items
System.IServiceProvider RequestServices
System.Threading.CancellationToken RequestAborted
System.String TraceIdentifier
Microsoft.AspNetCore.Http.ISession Session

Methods:
IFeatureCollection get_Features ()
HttpRequest get_Request ()
HttpResponse get_Response ()
ConnectionInfo get_Connection ()
AuthenticationManager get_Authentication ()
WebSocketManager get_WebSockets ()
ClaimsPrincipal get_User ()
Void set_User (ClaimsPrincipal value)
IDictionary`2 get_Items ()
Void set_Items (IDictionary`2 value)
IServiceProvider get_RequestServices ()
Void set_RequestServices (IServiceProvider value)
CancellationToken get_RequestAborted ()
Void set_RequestAborted (CancellationToken value)
String get_TraceIdentifier ()
Void set_TraceIdentifier (String value)
ISession get_Session ()
Void set_Session (ISession value)
Void Initialize (IFeatureCollection features)
Void Uninitialize ()
Void Abort ()
Type GetType ()
String ToString ()
Boolean Equals (Object obj)
Int32 GetHashCode ()
```

# Chapter V
## Exercise 01 – Breaking the rules

### Project structure:
```
d07_ex01\
      Program.cs
```

### The objective

Private fields cannot be changed. The end. It is impossible by design, it is bad and in most countries it is punishable by law.

But any knowledge gives us strength. Including knowledge of how to break some rules. And so we learn that the value of a private field can be changed at runtime using reflection.

Let's take the actual [DefaultHttpContext](<https://github.com/dotnet/aspnetcore/blob/c660e9b1e3d6918e327499d340cbc38065e34436/src/Http/Http/src/DefaultHttpContext.cs>) class again. It is contained in the Microsoft.AspNetCore.Http nuget package and is actively used in ASP.NET Core to store and manage information about http requests and responses.

First of all, create an instance of the DefaultHttpContext class. Output the value of its Response property to the console. You will see the full name of the DefaultHttpResponse type, this is how the ToString() method worked [by default](<https://docs.microsoft.com/ru-ru/dotnet/api/system.object.tostring?view=net-5.0#the-default-objecttostring-method>).

The Response property does not store anything in itself, it **encapsulates** the private _response field, the value of which is not directly accessible (this is the meaning of privacy). When a DefaultHttpResponse instance is created, a DefaultHttpResponse object is created and stored in the _response field. We output it above.

Let's change its value.

Get an object of the FieldInfo type for the _response field. Don't forget about the flags and the fact that the field is private and not static. The knowledge gained in the exercise above will help you with this.

Now use the [SetValue method](<https://docs.microsoft.com/ru-ru/dotnet/api/system.reflection.fieldinfo.setvalue?view=net-5.0#System_Reflection_FieldInfo_SetValue_System_Object_System_Object_>) and specify the new value - null. Output the value of the Response property to the console again. An empty string will be output.

So, we have changed the value of the private field. Keep in mind, most likely this modification will break the behavior of the DefaultHttpContext class. That’s why, use this knowledge with caution.

### Output format

```
Old Response value: {value}
New Response value: {value}
```

### Examples of launching an application from the project folder

```
$ dotnet run
Old Response value: Microsoft.AspNetCore.Http.Internal.DefaultHttpResponse
New Response value:
```

# Chapter VI
## Exercise 02 – Autofill

### Project structure:
```
d07_ex02/
      Attributes/
              NoDisplayAttribute.cs
      ConsoleSetter/
              ConsoleSetter.cs
      Models/
              IdentityUser.cs
              IdentityRole.cs
      Program.cs
```

### The objective

What if we let the user fill in some object? What if we don't need to know what kind of object it is, but with the help of reflection, your code can run through the properties of the class itself and fill in the necessary ones?

Let's try it.

Let's take the IdentityUser class. Put it into the project:

```
public class IdentityUser
{
   public IdentityUser()
   {
   }

   public IdentityUser(string userName) : this()
   {
       UserName = userName;
   }

   public virtual string UserName { get; set; }
   public virtual string NormalizedUserName { get; set; }
   public virtual string Email { get; set; }
   public virtual string NormalizedEmail { get; set; }
   public virtual bool EmailConfirmed { get; set; }
   public virtual string PasswordHash { get; set; }
   public virtual string SecurityStamp { get; set; }
   public virtual string ConcurrencyStamp() => Guid.NewGuid().ToString();
   public virtual string PhoneNumber { get; set; }
   public virtual bool PhoneNumberConfirmed { get; set; }
   public virtual bool TwoFactorEnabled { get; set; }
   public virtual DateTimeOffset? LockoutEnd { get; set; }
   public virtual bool LockoutEnabled { get; set; }
   public override string ToString()
       => $"User: {UserName}, {Email}, {PhoneNumber}";
}
```

This class is based on the [IdentityUser](<https://github.com/aspnet/AspNetIdentity/blob/main/src/Microsoft.AspNet.Identity.EntityFramework/IdentityUser.cs>) type from ASP.NET Identity. This class is used for user authorization and authentication in Web-applications.

Let's implement an application that, when launching, it will request data from the user to fill in this class. Let's figure it out in detail.

In the first exercise we learned how to get a list of type properties, in the second-to set the values of these properties. Create a ConsoleSetter class, it will be responsible for filling data through the console in the SetValues <T> (T input) method. Note that the method shouldn't care what kind of object it is being asked to fill, so we make the type T **generic**. However, further it will be important for us that this type is a class, let's add a [constraint](<https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/where-generic-type-constraint>).

Implement the *SetValues* method. First, let it output the greeting "Let's set {typeName}!" to the console, indicating which class we are going to fill in now. Now, for each public and non-static property, output the request "Set {propertyName}:" to the console and set the value entered by the user for this request to this property.

Now we can fill the "form" without the need to write each specific output. Let's set it up.

It is inconvenient and unnecessary to enter most of the values. Let's define that to fill in the class, we need to ask the user to fill in only the *UserName*, *Email* and *PhoneNumber* properties.
The **attributes** will help us mark the properties that are not required for filling in.

Attributes in C# let you add various **metadata** to a type. In .NET, attributes are used everywhere. For example, attributes from the **System.ComponentModel.DataAnnotations** namespace are used to validate models. **Xunit** actively uses attributes as markers, for example, to determine which method is a test and which test is with parameters.

In addition, attributes are often used for dynamic type identification at runtime and for formatted output. This is what we will need.

Let's declare [our attribute type](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/creating-custom-attributes) *NoDisplayAttribute*, inheriting it from **System.Attribute**. We will also specify the [conditions for its use](https://docs.microsoft.com/en-us/dotnet/standard/attributes/writing-custom-attributes): it should be available for use only with properties, the same attribute can be specified for a property only once. Mark all the properties of the IdentityUser class with this attribute, except *UserName*, *Email*, and *PhoneNumber*.

Now add filtering to *SetValues*: we only need output and input for properties that are not marked with the *NoDisplayAttribute* attribute. You can do this by accessing the [attribute collection](<https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/accessing-attributes-by-using-reflection>) for each property. Check yourself: only three properties should be requested for *IdentityUser*.

Now let's make it beautiful. Use the existing attributes **DescriptionAttribute** and **DefaultValueAttribute**, specify them for the properties *User name (User name, "Me")*, *Email (Email address, "test@test")* and *Phone Number (Phone number, "1234567890")* of the *IdentityUser* class. Now modify *SetValues* so that when requesting data, a request with the contents of **DescriptionAttribute** is output, and if the user has not entered data, the property is filled with the default value specified in **DefaultValueAttribute**.

It only remains to add a message about successful completion to the console and output the data using the ToString () method.

Check yourself: pass an instance of another class to *SetValue* instead of an *IdentityUser* instance:

```
public class IdentityRole
{
   public IdentityRole()
   {
   }

   public string Name { get; set; }
   public string Description { get; set; }

   public override string ToString()
      => $"{Name}, {Description}";
}
```

### Output format

```
Let's set {typeName}
Set {propertyDescription}:

Set {propertyDescription}:

Set {propertyDescription}:
...

We've set our instance!
```

### Examples of launching an application from the project folder

```
$ dotnet run
Let's set IdentityUser!
Set User name:
Ann
Set Email address:
ann@test.ru
Set Phone number:
123000123

We've set our instance!
User: Ann, ann@test.ru, 123000123
```

```
$ dotnet run
Let's set IdentityUser!
Set User name:

Set Email address:

Set Phone number:


We've set our instance!
User: Me, test@test, 1234567890
```

```
$ dotnet run
Let's set IdentityRole!
Set Name:
Moderator
Set Description:
A role for moderation

We've set our instance!
Moderator, A role for moderation
```

# Chapter VII
## Exercise 03 – Creating objects

### Project structure:
```
d07_ex03/
      Models/
              IdentityUser.cs
              IdentityRole.cs
      Program.cs
      TypeFactory.cs
```

### The objective

Sometimes, when using reflection, it is necessary to create an instance of a certain class. For example, when implementing your own **DI container**, when you register your types in it that you want to use for dependency injection. There can be a lot of ways to use it. But since the action takes place in runtime, and the type to be created is also known only in runtime, you don't have the option to use the **new () keyword**.

How do we create an instance of an object in runtime?

Let's implement the *TypeFactory* **factory** class, which will provide a set of **static** and **generic** methods:

- CreateWithConstructor<T>
- CreateWithActivator<T>

In each of them, we will use a separate method for creating an instance of the T class. Moreover, it will be important for us that T is a class, let's add a [constraint](<https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/where-generic-type-constraint>).

*CreateWithConstructor* will let you create an object of a type using the object's constructor. To implement it, it is necessary [to get an object of the ConstructorInfo type](<https://docs.microsoft.com/en-us/dotnet/api/system.type.getconstructor?view=net-5.0>) using reflection. For simplicity, we will use a parameterless constructor, this option is described in the given article. Next, use the **Invoke** method - this will let you call the class constructor and get the created object.

*CreateWithActivator* will let you create an object of the type using the [methods](<https://docs.microsoft.com/en-us/dotnet/api/system.activator?view=net-5.0>) of the static **Activator** class.

Copy the *IdentityUser* and *IdentityRole* classes from Exercise 02 to the project.
Use *TypeFactory* to create two objects of the *IdentityUser* type. We remind you that for simplicity when creating, we use a paramaterless constructor, but *ConstructorInfo* and *Activator* also let you create objects using a constructor with parameters. Output "user1 == user2" if the objects are equal and " user1 != user2" in the opposite case.

Do the same for the *IdentityRole* class.

Think about what output  should be correct here and [why](<https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/equality-operators#reference-types-equality>)?

# Chapter VIII
## Bonus task

Add the *CreateWithParameters<T>* method to the *TypeFactory* class so that it accepts an array of objects (*object*) and, using the *Activator* class method, which allows passing parameters to the constructor, creates an instance of the class using a parameterized constructor.
Create an object of the *IdentityUser* type with the specified UserName and output its name.

### Output format

```
$ dotnet run
d07_ex03.Models.IdentityUser
user1 != user2
d07_ex03.Models.IdentityRole
role1 != role2
```

### Output format (Bonus task)

```
$ dotnet run
d07_ex03.Models.IdentityUser
user1 != user2
d07_ex03.Models.IdentityRole
role1 != role2

d07_ex03.Models.IdentityUser
Set name:
Activated user
Username set: Activated user
```
