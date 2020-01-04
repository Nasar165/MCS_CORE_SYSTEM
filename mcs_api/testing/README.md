# Unit Testing Codeing Guidline 

### Intro
Unit testing is a critical component to the success of the project.  Unit testing keeps
the framework maintainable and less prone to bugs and unforeseen exceptions. To simplify
the process of writing a unit test we at MCS has introduced a set of rules. Making it easy
for us all involved to read and examine the test and if needed adjust a test to make it 
more reliable.

### Comments
A developer committing code must only apply comments if necessary.  Mainly because comments
might become outdated, confusing other developers trying to improve an already exisitng test. 
To combat comments please choose good variable, method and class names instead of adding 
comments.

## Unit test Class and method structure

### Sample
```` csharp
namespace mcs.components.test
{
    [TestClass]
    public class AesEncryptionTest
    {
        public AesEncryptionTest()
        {
            AesEncrypter._instance = new AesEncrypter("b14ca5898a4e4133bbce2ea2315a1916");       
        }
        
        [TestMethod]
        public void EncryptData()
        {
           var unEncryptedText = "Nasar is the greatest";
           var encryptedText = AesEncrypter._instance.EncryptData(unEncryptedText);
           Assert.AreNotEqual(unEncryptedText,encryptedText);
        }

        [TestMethod]
        public void DecryptData()
        {
            var unEncryptedText = "Nasar is the greatest";
            var encryptedText = AesEncrypter._instance.EncryptData(unEncryptedText);
            var decryptedText = AesEncrypter._instance.DecryptyData(encryptedText);
            Assert.AreEqual(unEncryptedText,decryptedText);
        }
    }
}

````

## Type of tests
There are many ways to test a method, in this case, we might test that a method is working 
as intended called a positive test where we predict a positive outcome. Or we might perform
a negative test where we intentionally attempt to break a method by inserting incorrect values.
Negative Test helps us to find bugs within our code making the framework more stable.

### Positive tests
A positive test aims at testing a method or class that it functions as intended. The test's 
the main focus is to provide a positive result showing that all is working as it should given 
that all the inputs are correct.
#### Sample
```` csharp
[TestMethod]
public void EncryptData()
{
   var unEncryptedText = "Nasar is the greatest";
   var encryptedText = AesEncrypter._instance.EncryptData(unEncryptedText);
   Assert.AreNotEqual(unEncryptedText,encryptedText);
}
````

### Negative Tests
Negative tests your class or method for faults, it's designed to break your code. This is
helpful when trying to identify bugs within your code and what better way then to break it
yourself. So a succesfull negative test is where your code breaks and wont give the expected
result.
#### Sample
```` csharp
[TestMethod]
public void EncryptNullvalue()
{
   try
   {
      var encryptedText = AesEncrypter._instance.EncryptData(null);
   }
   Catch(Exception error)
   {
      Assert.AreEqual("The parsed value can not be null",error.message);
   }  
}
````

