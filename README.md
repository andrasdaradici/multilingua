# MultiLingua
Simplify Unity translation file management with this tool for effortless organization and loading.​

Includes: 
- [NewtonsoftJson](https://github.com/JamesNK/Newtonsoft.Json)
- [YamlDotNet](https://github.com/aaubry/YamlDotNet)

Also available on:
- [Itch.io](https://andrasdaradici.itch.io/multilingua)

# Features
- Custom scriptable objects called "Language Items" for every language which include the following properties:
  - Name of language.
  - Font of language. (Japanese, Hindi, Urdu, etc. may use different fonts.)
  - Path to the language file. (You do not have to add the extension of the file, just enter the path and the script will do the rest for you)
  - Ability to choose between XML and JSON format.
  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/b9b69c05-3d29-4e0b-8a18-9a7cd0310a65)
- Update notice whenever there's an available update to the asset.
- Error messages
  - Check if the path to the Language Items is correct or not.

  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/a9bb6cef-3f62-4005-b9c9-dcae10362ab6)
  
  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/96f39eef-b46c-43f8-a039-88e25c7a8e5a)
  
- Check if the file path inside the Language Item is valid or not and if the provided keys are valid or not. (They only show up in the inspector when you run the project, it also works for the JSON format if I'd images for those also there'd be too many of them.)

  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/8fc5b9a6-ebe4-474b-81ac-e132770103d1)

  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/5d959db4-5575-4da2-a1d2-f5ce28c12c63)

  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/a0edbc6e-16a7-4e21-9a10-8d5b377dc830)

  ![image](https://github.com/andrasdaradici/multilingua/assets/90605554/6175ea1a-7f49-439b-ba7f-7ed8422b6ccc)

- Automatically add Language Items whenever you are in play mode
- Language gets stored in the PlayerPrefs under the "CurrentLanguage" as an integer so you do not have to worry about saving the language.

# Installation

**<ins>Method A:</ins>** Importing the `.unitypackage` file.

**Step 1:** - Go to the releases tab in the repository

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/fab23d6c-0db3-4e3b-a1fa-7f754c6623ba)

**Step 2:** - Select the version you want to use (The latest version is 1.1.0, that is recommended as it adds YAML support and it makes the Language Item objects prettier)

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/00c2bd3e-0243-4fd2-8fcb-be394cb5dc38)

**Step 3:** - Download the `.unitypackage` file.

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/7f91f6f5-68cd-43b6-9cf7-45b0c13b074f)

**Step 4: ** - Locate the downloaded file on your computer and double-click it to import it into Unity, you should see this:
Note: If you do not want to import the examples untick the box next to the "Examples" folder.

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/408a15fb-9caa-4091-82c7-a99fbbc99bde)

**<ins>Method B:</ins>** - Downloading the source code

**Step 1:** - Click on the "Code" and then on the "Download Zip" button on the repositories page

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/39389138-5b51-4ee4-9dda-2cf78938d480)

**Step 2:** - After downloading the source code, locate it and extract it from the .zip archive

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/367f43b6-2d1c-4c7d-9a82-31d4e9478060)

**Step 3:** - Locate the "MultiLingua" and "Plugins" folder and drag them into the "Assets" folder of your project
Note: Do NOT remove the Plugins folder as this will break the whole code, the code uses JSON.NET to read the data from `.json` files.

![image](https://github.com/andrasdaradici/multilingua/assets/90605554/7b282fdb-0dc2-45a5-8fdc-fd7c70b214ac)

# Usage
The usage of this asset will be shown in the wiki through the examples provided. 

[Check out the wiki!](https://github.com/andrasdaradici/multilingua/wiki/01-Overview)
