<a name="readme-top"></a>

<div align="center">
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
</div>

<!-- PROJECT LOGO -->
<br/>
<div align="center">
  <a href="https://github.com/alessandrochipulina/boutique/tree/main">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
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

[![Screen 1][screen1]]

* 1. El usuario inicia sesión ya sea en una aplicación móvil o en navegador. Internamente se envía una petición POST con el usuario y contraseña del usuario.
* 2. El servidor valida el usuario y contraseña enviados y genera un token, el cual es básicamente una cadena codificada donde agrega información como el Id del usuario, los roles que tiene el usuario, y el tiempo en el cual es token es válido por ejemplo 1 hora, 2 horas, 1 día, una vez caducado el token el usuario debe volver a iniciar sesión o pedir una renovación del token.
* 3. El navegador o la aplicación recibe el token y lo guarda. Se puede guardar el token en el local storage,cookies seguras, de la página si es una aplicación web o en los datos tu aplicación móvil. Por motivos de seguridad si guardas el token en el local storage debes agregar otra validación como por ejemplo la ip de la cual el usuario realizó el login, para que si un hacker obtiene el token e intenta acceder desde otra ciudad o país notificar al usuario para cancelar el token. :smile:
* 4. El usuario consulta alguna información del sistema, como por ejemplo la lista de clientes. En el servicio GET con la petición para la lista de clientes, en el header se envia el token que el servidor regreso en el paso 2.
* 5. El servidor válida el token si es válido y el usuario tiene permiso para consultar la información regresa la información, si no regresa un código de error (401) No autorizado.

Un Json Web Token es una cadena codificada en base64 formada por 3 partes las cuales están separadas por un punto.

Use the `BLANK_README.md` to get started.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

Se muestran las herramientas usadas para la creación de este proyecto:

* [![Next][Next.js]][Next-url]
* [![React][React.js]][React-url]
* [![Vue][Vue.js]][Vue-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Antes de comenzar

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* npm
  ```sh
  npm install npm@latest -g
  ```

### Installation

_Below is an example of how you can instruct your audience on installing and setting up your app. This template doesn't rely on any external dependencies or services._

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```js
   const API_KEY = 'ENTER YOUR API';
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Add Changelog
- [x] Add back to top links
- [ ] Add Additional Templates w/ Examples
- [ ] Add "components" document to easily copy & paste sections of the readme
- [ ] Multi-language Support
    - [ ] Chinese
    - [ ] Spanish

See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a full list of proposed features (and known issues).

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
## Contact

Your Name - [@your_twitter](https://twitter.com/your_username) - email@example.com

Project Link: [https://github.com/your_username/repo_name](https://github.com/your_username/repo_name)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

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
[screen1]: images/screen1.png
[screen2]: images/screen2.png
[screen3]: images/screen3.png
[screen4]: images/screen4.png
[Next.js]: https://img.shields.io/badge/-.NET%207.0-blueviolet
[Next-url]: https://dotnet.microsoft.com/es-es/download/dotnet/7.0
[React.js]: https://img.shields.io/badge/-SWAGGER-green
[React-url]: https://swagger.io/
[Vue.js]: https://img.shields.io/badge/-MSSQL-yellow
[Vue-url]: https://www.microsoft.com/es-es/sql-server/sql-server-downloads
