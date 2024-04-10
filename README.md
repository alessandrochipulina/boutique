<a name="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br/>
<div align="center">
  <a href="https://github.com/alessandrochipulina/boutique/tree/main">
    <img src="image/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">BOUTIQUE XYZ</h3>

  <p align="center">
    REST API Services with .NET 7.0 and JWT Bearer Token Validation !!
    <br />
    <a href="https://github.com/alessandrochipulina/boutique/tree/main"><strong>Explore the docs »</strong></a>
    <br />
    <br />    
    ·
    <a href=https://github.com/alessandrochipulina/boutique/tree/main/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/alessandrochipulina/boutique/tree/main/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## Acerca del proyecto

Una forma común de manejar la seguridad en tus servicios, es mediante la seguridad basada en tokens. En el cual después de que el usuario inicia sesión el servidor regresa una cadena codificada con los datos que identifican al usuario(claims), este token se debe enviar en cada petición a los servicios rest, el servidor valida el token si es correcto obtiene los datos del usuario y si el usuario tiene el permiso o el rol correcto muestra la información, una explicación de forma gráfica es la siguiente:

![Screen 1][screen1]

* El usuario inicia sesión ya sea en una aplicación móvil o en navegador. Internamente se envía una petición POST con el usuario y contraseña del usuario.
* El servidor valida el usuario y contraseña enviados y genera un token, el cual es básicamente una cadena codificada donde agrega información como el Id del usuario, los roles que tiene el usuario, y el tiempo en el cual es token es válido por ejemplo 1 hora, 2 horas, 1 día, una vez caducado el token el usuario debe volver a iniciar sesión o pedir una renovación del token.
* El navegador o la aplicación recibe el token y lo guarda. Se puede guardar el token en el local storage,cookies seguras, de la página si es una aplicación web o en los datos tu aplicación móvil. Por motivos de seguridad si guardas el token en el local storage debes agregar otra validación como por ejemplo la ip de la cual el usuario realizó el login, para que si un hacker obtiene el token e intenta acceder desde otra ciudad o país notificar al usuario para cancelar el token. :smile:
* El usuario consulta alguna información del sistema, como por ejemplo la lista de clientes. En el servicio GET con la petición para la lista de clientes, en el header se envia el token que el servidor regreso en el paso 2.
* El servidor válida el token si es válido y el usuario tiene permiso para consultar la información regresa la información, si no regresa un código de error (401) No autorizado.

Un Json Web Token es una cadena codificada en base64 formada por 3 partes las cuales están separadas por un punto.

* Header: Indica el algoritmo y tipo de token
* Payload: Datos del usuario, caducidad del token, roles del usuario
* Signature: Incluye una llave secreta para validar el token

Para poder generar los tokens necesitamos:
* LLave secreta: Es una llave que permite encriptar/desencriptar la información del token
* Issuer: Es quien genera el token, por lo general es la URL del servidor que contiene los servicios

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- BUILD WITH -->
### Construido con

Se muestran las herramientas usadas para la creación de este proyecto:

* [![Next][Next.js]][Next-url]
* [![React][React.js]][React-url]
* [![Vue][Vue.js]][Vue-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Antes de comenzar

Se deberán de realizar algunos pasos previos.

### Prerequisitos

1. Se debe de contar con el acceso a un servidor de base de datos MS SQL Server.
2. Contar con Visual Studio 2022 con NET 7.0 instalado.

### Instalación

1. Clonar este repositorio
   ```sh
   git clone https://github.com/alessandrochipulina/boutique.git
   ```
3. Instalar la base de datos BoutiqueXYZ desde los archivos de la ruta
   ```sh
   src\resources\sql
   ```
4. Establecer las siguientes variables de entorno
   ```cs
   ASPNETCORE_ENVIRONMENT = 'Development'
   META_APP_NAME = 'Boutique'
   META_LOG_PATH = 'D:\Code\Boutique.log'
   META_ENV = 'Development'
   META_API_KEY = '123'
   META_LOG_BASE_LEVEL = 'error'
   META_LOG_MICROSOFT_LEVEL = 'error'
   SQL_HOST = 'localhost'
   SQL_PORT = '1433'
   SQL_USER = 'sa'
   SQL_PASSWORD = 'A$1234aaaa'
   SQL_DATABASENAME = 'BoutiqueXYZ'
   TOKENS_KEY = 'QWERTYUIOPASDFGHJKLZXCVBNMQWERTY'
   TOKENS_ISSUER = 'https://localhost:61925'
   ```
5. Correr la aplicación. Acceder a la ruta https://localhost:61925/swagger/index.html
   
6. Abrir la colección de la carpeta
  ```sh
   src\resources\postman
   ```
   con la aplicación POSTMAN y ejecutar el LOGIN

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

Se muestran pantallas de su uso:

![Screen 2][screen2]

![Screen 3][screen3]

![Screen 4][screen4]

_For more examples, please refer to the [Documentation](https://github.com/alessandrochipulina/boutique)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Hoja de Ruta

- [x] Agregar hoja de cambios
- [x] Add back to top links
- [ ] Agregar ejemplos adicionales
- [ ] Soporte Multi Lenguaje
    - [ ] Chinese
    - [ ] English

Vea la lista [open issues](https://github.com/alessandrochipulina/boutique) para una lista completa de propuestas recomendadas.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contacto

Alessandro Chipulina - [@your_twitter](https://twitter.com/achipulina) - alessandro.chipulina@gmail.com

Project Link: [https://github.com/alessandrochipulina/boutique](https://github.com/alessandrochipulina/boutique)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ACKNOWLEDGMENTS -->
## Agradecimientos

Use this space to list resources you find helpful and would like to give credit to. I've included a few of my favorites to kick things off!

* [Choose an Open Source License](https://choosealicense.com)
* [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
* [Malven's Flexbox Cheatsheet](https://flexbox.malven.co/)
* [Malven's Grid Cheatsheet](https://grid.malven.co/)
* [Img Shields](https://shields.io)
* [GitHub Pages](https://pages.github.com)
* [Font Awesome](https://fontawesome.com)
* [React Icons](https://react-icons.github.io/react-icons/search)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/alessandrochipulina/boutique/tree/main
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/alessandrochipulina/boutique/tree/main
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/alessandrochipulina/boutique/tree/main
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/alessandrochipulina/boutique/tree/main
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]:https://github.com/alessandrochipulina/boutique/tree/main
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/chipulina/
[screen1]: image/screen1.png
[screen2]: image/screen2.png
[screen3]: image/screen3.png
[screen4]: image/screen4.png
[Next.js]: https://img.shields.io/badge/-.NET%207.0-blueviolet
[Next-url]: https://dotnet.microsoft.com/es-es/download/dotnet/7.0
[React.js]: https://img.shields.io/badge/-SWAGGER-green
[React-url]: https://swagger.io/
[Vue.js]: https://img.shields.io/badge/-MSSQL-yellow
[Vue-url]: https://www.microsoft.com/es-es/sql-server/sql-server-downloads
