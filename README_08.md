# Day 08 – .NET Boot camp
### Testing

# Contents
1. [Chapter I](#chapter-i) \
	[General Rules](#general-rules)
2. [Chapter II](#chapter-ii) \
	[Rules of the Day](#rules-of-the-day)
3. [Chapter III](#chapter-iii) \
	[Intro](#intro)
4. [Chapter IV](#chapter-iv) \
	[Exercise 00 – ElementsTests](#exercise-00-elementstests)
5. [Chapter V](#chapter-v) \
  [Exercise 01 – MarkdownBuilderTests](#exercise-01-markdownbuildertests) 
6. [Chapter VI](#chapter-vi) \
  [Exercise 02 – MarkdownableTypeTests](#exercise-02-markdownabletypetests) 
7. [Chapter VII](#chapter-vii) \
  [Exercise 03 – GithubWikiDocumentBuilderTests](#exercise-03-githubwikidocumentbuildertests)

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


# Chapter III
## Intro

Programming is a profession where you always need to learn. New tools, technologies, methodologies, you have to quickly understand all these. You have already noticed that with the growth of experience, the solution of some tasks comes easier, faster and better.

The only thing that takes a lot of time is debugging. It is not convenient and very wasteful to check the software operation scenario one by one every time. And here you are, inspired by Kent Beck and his book [Test Driven Development: By Example](<https://www.amazon.com/Test-Driven-Development-Kent-Beck/dp/0321146530>), you decide to help the OpenSource community and write tests for the [markdown-generator](<https://github.com/smarthead/markdown-generator>) library, which you plan to use on the next project.

This library lets you generate documentation for the code in **Markdown** format, which can later be posted on the GitHub Wiki. You can see an example [here](<https://github.com/smarthead/markdown-generator/wiki>).

You need to clone the repository to your computer, and change the origin to the repository that you created for this exercise.

```
$ git remote rm origin
$ git remote add origin your/repository/project.git
$ git config master.remote origin
$ git config master.merge refs/heads/master
```
### Types of tests

Different types of tests are used at different levels of the software. For example, **unit-tests** are used to test methods and functions that are a small part of the whole. While **integration tests** check the interaction of the system modules. You can learn a little more about the types of tests [here](<https://en.wikipedia.org/wiki/Software_testing#Testing_levels>).

### Methodologies

In addition to the types of testing, there are various approaches to it.

In this lesson, we will write **BDD** tests-tests based on the current behavior of the system. Besides, there is an approach called **TDD** - Test Driven Development. You can read about the pros and cons of the approaches [here](<https://www.browserstack.com/guide/tdd-vs-bdd-vs-atdd>).

### What to test?

It should be understood that tests are also part of the code base that needs to be maintained. Your efforts, hours and employer's money are also spent on support, updating this code base. Therefore, you should ask the question, what is the purpose of testing?

There is an interesting [article](<https://blog.ploeh.dk/2018/11/12/what-to-test-and-not-to-test/>) on this topic. As in the article, in our case we will protect ourselves from the **regression** of the project.

### Best practices

The [Microsoft](<https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices>) website contains brief information on all of the above points, and there is also a section on the correct naming of tests. It is a must-study.

### Frameworks

Of course, tests are not written from scratch. There are several basic frameworks for their development. For unit-tests, these are **XUnit**, **NUnit** and **MsTest**. You can read more about each of them [here](<https://www.lambdatest.com/blog/nunit-vs-xunit-vs-mstest/#:~:text=as%20NUnit%20vs.-,XUnit%20vs.,of%20the%20%5BTest%5D%20attribute.>).

For this project, we will choose XUnit, since it is actively supported by the community and incorporates the best that is in other frameworks. See how to run unit-tests in the IDE where you are developing projects.

### Project structure

Create a *Tests* folder in the solution with the library, create a *Markdown.Generator.Core.Tests* project in the *Tests *folder using **the Unit Test Project template** (the name depends on the IDE used). Specify XUnit as the framework to use. All tests of these exercises must be placed in this project. The name of each exercise is the name of the test file.

```
Markdown.Generator/
      Tests/
            Markdown.Generator.Core.Tests/
                  ElementsTests.cs
                  MarkdownBuilderTests.cs
                  MarkdownableTypeTests.cs
                  GithubWikiDocumentBuilderTests
      Markdown.Generator.Core/
      Markdown.Generator.Application/
```

### Instruction

The [markdown-generator](<https://github.com/smarthead/markdown-generator>) library does contain a number of bugs. As part of writing tests, you will meet with them, and they will need to be corrected.

There are different conventions for naming tests, but we will use the BDD-specific one:
Given_Preconditions_When_StateUnderTest_Then_ExpectedBehavior.


# Chapter IV
## Exercise 00 – ElementsTests

In the [markdown-generator](<https://github.com/smarthead/markdown-generator>) library, the final document in the Markdown format is formed using small building blocks, such as *Header*, *Link*, *Table*, and others. These elements encapsulate the logic of formatting the passed parameters in the Markdown markup element.

There are a lot of elements - it is easy to make mistakes in the syntax during implementation, and the finished document will contain an error. But you won't notice it right away, because the documentation can be quite voluminous. 

So first we will write tests for all the elements from the *Markdown.Generator.Core.Elements* namespace that will check that the markup matches the Markdown markup.

Let's look at the example of the *Code* element. This element is for displaying source code, including formatting and syntax highlighting. In Markdown, this element looks like this:

````
```csharp
some code
```
````
As we can see, this element consists of a sequence of characters ```` ``` ````, the name of the programming language, directly the code to display and ends with the same sequence of characters ```` ``` ````.

In C#, this line, considering line breaking, will look like:

````
```csharp\nsome code\n```\n
````

Create a test in the *ElementsTests* file.

Following the test naming convention, the test name will be as follows:
Given_Code_When_LanguageAndCodeAsParameter_Then_ReturnMarkdownCodeMarkup.

Next, take the line ```` ```csharp\nsome code\n```\n ```` as a reference and put its value in the *expected* variable. Then create an object of the *Code* type with the ```csharp``` and ```some code``` parameters and call the *Create* method. Save the result to the *actual* variable. Now, to check the value, use the static **Assert** class and compare the two values. If the values are not equal, the test will fail. 

The actions described above are a pattern of building unit-tests, which is called **AAA-Arrange**, **Act**, **Assert**. The **Arrange** block configures the unit-test environment (creating a *Code* object, *expected* declaration). The test script is executed in the **Act** block. And in the **Assert** block, the results are checked. Follow this pattern, and your tests will be clean, they will be easy to read.

Write tests of the *CodeQuote*, *Header*, *List*, *Link*, *Image* elements. You can take examples of return values from the [library documentation](<https://github.com/smarthead/markdown-generator/wiki/Markdown.Generator.Core.Elements#code>) and use them as expected values when writing tests. 

You can find more information about markup in [Markdown-Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#headers).


# Chapter V
## Exercise 01 – MarkdownBuilderTests

Let's take a look at the *MarkdownBuilder* class. This class implements the [builder](<https://en.wikipedia.org/wiki/Builder_pattern>) pattern for building the markdown-markup of a document from markup elements.

The [corresponding methods](<https://github.com/smarthead/markdown-generator/wiki/Markdown.Generator.Core#markdownbuilder>) are implemented to add elements to the document. Since there are many elements, it is easy to make a mistake in the implementation, for example, in the *CodeQuote* method to create a *Code* element.

*MarkdownBuilder* allows you to view the list of added elements through the *Elements* property. 

It is necessary to implement unit-tests which check that after calling the *CodeQuote*, *Code*, *Link*, *Header* methods of the *MarkdownBuilder* class, elements of the same types fall into the *Elements* collection. To pass the tests successfully, you need to check that only one element is added to the Elements collection, which has the *CodeQuote*, *Code*, *Link*, *Header* type (depending on which element the test is written for). The static **Assert** class and its *Equals* and *Contains* methods will help with this.


# Chapter VI
## Exercise 02 – MarkdownableTypeTests

The *MarkdownableType* class is a wrapper over **System.Type**, which implements methods for convenient access to information about class members, such as **FieldInfo**, **MethodInfo**, **PropertyInfo**, and others. More information can be found [here](<https://github.com/smarthead/markdown-generator/wiki/Markdown.Generator.Core#markdownabletype>).


In addition, the class overrides the **ToString** method - when it is called, type information is returned in the form of a markdown string.

The class contains the *GetMethods* method, which returns an array of MethodInfo - information about methods for documentation. But there is a problem, the documentation should not specify private methods, since they are part of the internal api of the software and change frequently. These changes should not affect the Software users. But this method returns them.

Before looking for an error, you need to write a test. To do this, we need an object that has a list of public and private methods that we know in advance. This object will be called **stub**, it will simulate the specified state.

For this test, create a *Sut* class in the same file with *MarkdownableTypeTests*

``` 
public class Sut
{
   public void PublicMethod(){ }
   private void PrivateMethod(){ }
}
```

Write a test that creates a *MarkdownableType* object for the Sut type, and call the *GetMethods* method. Make sure that the result contains only the *PublicMethod* method, for example by name, or using **MethodInfo.isPrivate**.

The test will fail with an error. Now edit the code of the *GetMethods* method so that the test runs successfully. 

Additionally, write similar tests for the *GetFields* and *GetProperties* methods.


# Chapter VII
## Exercise 03 – GithubWikiDocumentBuilderTests

The project has a *GithubWikiDocumentBuilder* class, which is responsible for generating documentation files for later posting on the Github Wiki. The class is **generic**, and takes into the constructor an object that implements the *IMarkdownGenerator* interface.


When writing unit-tests for the *GithubWikiDocumentBuilder* class, we face a problem - in order to isolate the tests for *GithubWikiDocumentBuilder* from the implementation of *IMarkdownGenerator* and not depend on its implementation and side effects, we need to simulate the behavior of *IMarkdownGenerator*. To do this, as it was mentioned earlier, we can use a **stub**-object, for example, create an implementation of this interface that returns pre-prepared objects.

But if we need to test various work scenarios, such as exceptions, positive scenarios, or negative scenarios, it can be inconvenient to create **stub**-objects for each of the tests.

**Stub** simulates the state of the object. But to simulate different behavior, we will need objects of a different category - **Mock**-objects.

**Mock**-s simulate the behavior of an object, return the result of functions in the form of constants or a set of random values. In addition to providing the results, mock allows you to check whether the function was called, with what parameters and how many times.

.NET has a great library for creating mock objects - [Moq](https://github.com/moq/moq). As the documentation for the library says, it is enough for us to create an object of the **Mock** class and specify the interface that we want to mock as a generic type. Then, using the **Setup** and **Returns** methods, configure which methods we want to replace, and what results they should return.

Use the *Moq* library to implement the following tests.

Reflection can significantly affect the speed of the application. Why are we thinking about this? Because the *Load* methods in the class implementing the *IMarkdownGenerator* interface actively use reflection to find the collection of information about types.

It is necessary to check with the help of the Moq package that overloads of Load methods are called only once to generate one set of documentation (i.e. for one call of the *Generate* method). May **Verify** and **Times** help you.

Since we have several overloads of the *Load* method available, and all overloads are used in the *GithubWikiDocumentBuilder* class, we need to check that the correct overloads are called with the correct parameters.
