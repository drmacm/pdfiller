### Disclaimer
PDFiller is a work in progress, and the features described below are under development.
Current version serves only as a proof of concept for client-side PDF manipulation.

### About
PDFiller helps you create a simple static website based on your fillable PDF form.

#### Problem description
Imagine you have a fillable PDF form for which you want to create a nice-looking website. The website should contain an HTML form that represents the fields of your PDF form. After the visitor submits the HTML form, the PDF form should be automatically filled in based on the provided values, and the visitor should be offered to download the filled-in PDF form.

There are various reasons why you may want to create such a website:
- offering an improved design of the PDF form
- providing additional explanations for some of the fields
- making use of responsive design of the HTML form

Creating such a website requires:
- fronend website with a HTML form whose fields correspond to the PDF form
- backend service that accepts the data, fills the PDF form and offers it for download

Basically, improving presentaiton of a single PDF form requires a whole client-server solution, together with deployment and hosting.

#### Solution description

PDFiller solves this problem by helping you create a single-page static website which:
- contains the HTML form with the fields based on your PDF form
- fills the PDF form when the HTML form is submitted and offers it for download

All the functionality happens in the user's browser, which drastically reduces the complexity of the solution. Hosting is also much simpler because it's a static website.



### Technical details
- The solution is based on [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1#blazor-webassembly)
- PDF manipulation is based on [iText 7](https://github.com/itext/itext7-dotnet)


Licensed under [AGPL](LICENSE).