# PadelBackend v2

### IMPORTANTE

Siempre se hará referencia acerca de la url con "url/..." donde la 'url' puede ser el localhost como la url de producción dependiendo de donde se encuentre la aplicacion

### AUTH

Los usuarios podrán iniciar sesión ingresando el nombre de usuario o correo electrónico junto con la contraseña de la cuenta al siguiente endpoint

Cabe destacar la contraseña tendrá un tamaño minimo de 8 caracteres y el nombre de usuario contendrá solamente letras, numeros y guiones bajos empezando siempre por una letra.

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

También podrá cambiar los datos de su perfil realizando una peticion PUT al mismo url con los datos que quiere cambiar. Por ejemplo, si quisiera cambiar de nombre de usuario lo haria de la siguiente manera

```bash
PUT
url/api/user

{
  "name": "string"
}
```

El sistema extraerá del token de la request el usuario que desea cambiar sus credenciales y las actualizará automaticamente sin necesidad de enviar una id como prop

### RACKETS

Para conocer la información de una racketa o varias no es necesaria autenticación ya que es un recurso público

```bash
GET
url/api/racket

```
Donde retornará todas las raquetas dentro de la db

También se podra mediante querys dentro de la url se podrá hacer peticiones filtradas en función de lo que quiera el cliente en donde se tendrá en cuenta los siguientes parámetros

- Model (string)
- Brand (string)
- MinPrice (float)
- MaxPrice (float)
- Limit (int)

Un ejemplo sería

```bash
GET
url/api/racket/q=brand=adidas&limit=10
```
En este ejemplo traerá 10 racketas que sean de Adidas

Tambien podriamos pedirle unicamente la información de una unica raqueta mediante su id de la siguiente manera

```bash
GET
url/api/racket/:id
```

