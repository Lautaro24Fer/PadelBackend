# PadelBackend v1

La version v1 de la actual api contiene los siguientes endpoints 

### IMPORTANTE

Siempre se hará referencia acerca de la url con "url/..." donde la 'url' puede ser el localhost como la url de producción dependiendo de donde se 

### AUTH

Los usuarios podrán iniciar sesión ingresando el nombre de usuario o correo electrónico junto con la contraseña de la cuenta al siguiente endpoint

```bash
POST
url/api/auth

```

Se le retornará un bearer token que deberá usar en los encabezados de las peticiones para poder hacer las consultas que requiram una autorizacion previa

### USERS

En la parte de usuarios el unico recurso publico será el login en donde el usuario podrá ingresar sus credenciales para registrarse al sistema

```bash
POST
url/api/user

{
  "name": "string",
  "userName": "string",
  "email": "user@example.com",
  "password": "stringst"
}
```

Una vez autenticados se podrá consultar acerca de la informacion de los usuarios en los siguientes endpoints

```bash
GET
url/api/user

```
En donde mostrará un json de todos los usuarios cargados

```bash
GET
url/api/user/:id

```
En donde mostrará todos los detalles de un usuario determinado por su id

### RACKETS

De igual manera que los usuarios, se pueden consultar por todas o una unica racketa y crear una. Para la parte de la creacion de la racketa se requiere autoriazción. En donde se hace una POST request con los datos de la racketa al siguiente recurso

```bash
POST
url/api/racket

{
  "name": "string",
  "description": "string",
  "price": 0,
  "category": "string",
  "image": "string"
}
```
Para conocer la información de una racketa o varias no es necesaria autenticación ya que es un recurso público

```bash
GET
url/api/racket

```

En donde mostrará un json de todas las racketas cargadas

```bash
GET
url/api/racket/:id

```
En donde mostrará todos los detalles de un usuario determinado por su id




