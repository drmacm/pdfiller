Write-Host "Copying a solution with generated website to a separate folder."
$GeneratedWebsiteFolder = Read-Host -Prompt "Enter a folder name for your static website"

Copy-Item "..\PDFiller.Domain" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Domain" -Recurse
Write-Host "Successfully copied the PDFiller.Domain project."

Copy-Item "..\PDFiller.Website" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Website" -Recurse
Write-Host "Successfully copied the PDFiller.Website project."

Copy-Item "..\PDFiller.Website.sln" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Website.sln"
Write-Host "Successfully copied the solution file."

Copy-Item "..\..\LICENSE" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\LICENSE"
Write-Host "Successfully copied the license file."

Write-Host "Generated website is available in ""..\GeneratedWebsites\$GeneratedWebsiteFolder""."

