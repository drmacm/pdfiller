## About
PDFiller helps you create a simple static website based on your fillable PDF form.

### Problem description
Imagine you have a fillable PDF form for which you want to create a nice-looking website. The website should contain an HTML form that represents the fields of your PDF form. After the visitor submits the HTML form, the PDF form should be automatically filled in based on the provided values, and the visitor should be offered to download the filled-in PDF form.

There are various reasons why you may want to create such a website:
- offering an improved design of the PDF form
- providing additional explanations for some of the fields
- making use of responsive design of the HTML form

Creating such a website usually requires:
- fronend website with a HTML form whose fields correspond to the PDF form
- submitting the form sends the data from the HTML form via an API call
- backend service that accepts the data, fills the PDF form and offers it for download

Basically, creating a HTML counterpart for a single PDF form requires a whole client-server solution, together with deployment and hosting.

### Solution description

PDFiller solves this problem by helping you create a single-page static website which:
- contains the HTML form with the fields based on your PDF form
- fills the PDF form when the HTML form is submitted and offers it for download

All the functionality happens in the user's browser, which drastically reduces the complexity of the solution. Hosting is also much simpler because it's a static website.

## Technical details
- The solution is based on [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1#blazor-webassembly)
- PDF manipulation is based on [iText 7](https://github.com/itext/itext7-dotnet)

Licensed under [AGPL](LICENSE).

## Usage
Generating the website consists of 3 steps: website generation, website copying and cleanup.

### Website generation
To generate the website:
- clone the repository
- open PDFiller.WebsiteGenerator.sln
- set "PDFiller.WebsiteGenerator.ConsoleRunner" as your start-up project
- run the project

The program will prompt you for the path to the PDF form on your local hard drive, and will collect the information about the available form fields. For each of the fields, you need to choose if the field is required, optional, or if it should be skipped.

During the execution, the program will modify 3 files, adding the information collected from the PDF form:
- PDFiller.Website\Models\FormModel.cs
- PDFiller.Website\Pages\HtmlForm.razor
- PDFiller.Website\Services\PdfFormFillingService.cs

### Website copying
After the relevant files are modified, you need to copy the website to a separate folder in order to isolate it from the WebsiteGenerator context. 

To copy the website, run the script in "scripts\Copy-GeneratedWebsite.ps1".

It will prompt you for a [FOLDER_NAME] for your website and it will copy the files needed for running the static website into GeneratedWebsites/[FOLDER_NAME]. Files and folders that are copied:
- PDFiller.Models
- PDFiller.Website
- PDFiller.Website.sln
- LICENSE file

After the copying is done, go to GeneratedWebsites/[FOLDER_NAME] and open the PDFiller.Website.sln.

This code is now completely detached from the original solution, and can be treated as independent project.

#### Running the generated website
To run the newly created website, set the "PDFiller.Website" as the start-up project and run it.

You will see a single page web application with an HTML form, containing the fields from your PDF form.

If you fill up the fields correctly and submit the form, you'll get your PDF form filled in with data you typed in the HTML form.

#### Improving the generated website
The website will probably not look the way you want, so you'll need to do some additional work with the HTML. However, the main functionality of your website, filling up the form, is already working.

To continue working on your website, modify the code inside GeneratedWebsites/[FOLDER_NAME], making sure you respect the provided license.

### Cleanup
After you copied your website using the PowerShell script, undo the changes to the 3 modified files by doing `git reset --hard` or a similar command. After that, you can generate another website using a different PDF form.