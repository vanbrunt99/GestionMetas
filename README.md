Gestión de Metas Personales – ASP.NET Core MVC
1. Descripción general

Gestión de Metas es una aplicación web desarrollada como práctica/examen de Programación II 2025.
El sistema funciona como una agenda ejecutiva personal, permitiendo al usuario:

Definir metas principales (objetivos grandes).

Desglosarlas en tareas concretas y medibles.

Controlar estado, prioridad, fechas límite y dificultad.

Visualizar las tareas asociadas a cada meta en un panel claro y ordenado.

La aplicación está construida con:

ASP.NET Core MVC (.NET 8)

Entity Framework Core 8 (enfoque Code First)

SQL Server LocalDB como motor de base de datos

Interfaz web con Bootstrap y un tema oscuro tipo “agenda profesional ejecutiva”.

2. Objetivos del sistema

Practicar el desarrollo de una aplicación web completa con ASP.NET Core MVC.

Implementar el patrón Modelo–Vista–Controlador de forma clara y organizada.

Utilizar Entity Framework Core para:

Diseño del modelo de datos.

Creación y actualización de la base de datos mediante migraciones.

Manejo de relaciones uno a muchos (Meta → Tareas).

Aplicar validaciones de datos y buenas prácticas básicas de diseño.

Presentar una interfaz limpia, moderna y fácil de utilizar.

3. Requisitos técnicos

Sistema operativo: Windows 10 o superior.

IDE: Visual Studio 2022 (o superior) con el workload de “Desarrollo para ASP.NET y web”.

SDK .NET 8 instalado.

Motor de base de datos: SQL Server Express / LocalDB (incluido normalmente con Visual Studio).

Conexión a internet inicial para restaurar paquetes NuGet.

4. Estructura del proyecto

El proyecto sigue la estructura típica de una aplicación ASP.NET Core MVC:

Controllers/

HomeController.cs

MetasController.cs

TareasController.cs
Encargados de la lógica de flujo, recepción de solicitudes HTTP y coordinación entre modelo y vistas.

Models/

Meta.cs

Tarea.cs

Enums.cs (categorías, prioridades, estados y dificultad)
Contienen las clases de dominio y las anotaciones de datos para validación.

Data/

ApplicationDbContext.cs
DbContext de Entity Framework Core: define los DbSet y la configuración de la relación Meta–Tareas.

Migrations/

Archivos generados por Add-Migration que describen el esquema de la base de datos.

Views/

Home: Index.cshtml, Privacy.cshtml

Metas: Index.cshtml, Create.cshtml, Edit.cshtml, Details.cshtml, Delete.cshtml

Tareas: Index.cshtml, Create.cshtml, Edit.cshtml, Details.cshtml, Delete.cshtml

Shared: _Layout.cshtml, _ViewImports.cshtml, _ViewStart.cshtml, Error.cshtml
Contienen la interfaz de usuario y utilizan los modelos fuertemente tipados.

wwwroot/

css/site.css → hoja de estilos personalizada (tema oscuro).

Bibliotecas de cliente (Bootstrap, jQuery, etc.).

appsettings.json

Incluye la cadena de conexión DefaultConnection apuntando a SQL Server LocalDB.

Program.cs

Configuración inicial de la aplicación (servicios, DbContext, middlewares, rutas, etc.).

5. Modelo de datos
5.1. Entidad Meta

Representa una meta principal del usuario.

Campos importantes:

Id (int, clave primaria).

Titulo (string, requerido, 5–100 caracteres).

Descripcion (string?, opcional).

Categoria (enum Categoria: Desarrollo personal, Salud, Carrera, Finanzas, etc.).

FechaCreacion (DateTime, inicializada automáticamente a DateTime.Now).

FechaLimite (DateTime?, opcional).

Prioridad (enum Prioridad: Alta, Media, Baja).

Estado (enum EstadoMeta: No iniciada, En progreso, Completada, Abandonada).

Tareas (List<Tarea>): colección de tareas asociadas (relación uno-a-muchos).

Se utilizan anotaciones como [Required], [StringLength] y [DataType] para validar la entrada.

5.2. Entidad Tarea

Representa una tarea específica asociada a una meta.

Campos importantes:

Id (int, clave primaria).

Descripcion (string, requerida, 5–200 caracteres).

FechaCreacion (DateTime, inicializada a DateTime.Now).

FechaLimite (DateTime?, opcional).

Estado (enum EstadoTarea: Pendiente, En progreso, Completada).

Dificultad (enum Dificultad: Fácil, Media, Difícil).

TiempoEstimado (double, horas estimadas de trabajo).

MetaId (int, clave foránea).

Meta (propiedad de navegación hacia la Meta).

La relación se configura también en ApplicationDbContext para que, al borrar una Meta, se eliminen en cascada sus tareas.

5.3. Enums con nombres amigables

Se usan enumeraciones para:

Categoria

Prioridad

EstadoMeta

EstadoTarea

Dificultad

Cada valor tiene un [Display(Name = "...")] para mostrar textos legibles en la interfaz (por ejemplo, “Desarrollo personal” en lugar de “DesarrolloPersonal”).

6. Funcionalidades implementadas
6.1. Panel principal (Home)

Página de inicio tipo “dashboard ejecutivo”:

Título “Agenda Ejecutiva de Metas”.

Descripción corta de la idea del sistema.

Dos tarjetas principales:

Metas → acceso directo al CRUD de metas.

Tareas → acceso directo al CRUD de tareas.

6.2. Gestión de Metas (CRUD completo)

Ruta principal: /Metas

Index:

Lista todas las metas en una tabla.

Muestra título, categoría, prioridad, estado y fecha límite.

Acciones: Detalles, Editar, Eliminar.

Create:

Formulario para crear nuevas metas.

Validaciones de longitud, campos requeridos y fecha.

Edit:

Permite modificar datos de una meta existente.

Details:

Muestra la información completa de la meta.

Sección adicional donde se listan las tareas asociadas.

Botón para “Agregar tarea a esta meta”.

Delete:

Página de confirmación antes de borrar una meta.

Al eliminar una meta, sus tareas asociadas también se eliminan (borrado en cascada).

6.3. Gestión de Tareas (CRUD completo)

Ruta principal: /Tareas

Index:

Lista todas las tareas.

Muestra descripción, meta asociada, estado, dificultad, fecha límite y tiempo estimado.

Create:

Formulario para crear una tarea.

Se selecciona la Meta a la que pertenece desde un desplegable.

Puede invocarse desde /Tareas o desde los detalles de una Meta (en cuyo caso la meta llega preseleccionada).

Edit:

Modificación de los campos de la tarea (incluido el estado para marcar como Completada).

Details:

Muestra información detallada de una tarea individual.

Delete:

Confirmación antes de eliminar una tarea.

7. Interfaz y diseño (tema oscuro)

El proyecto utiliza Bootstrap y un conjunto de estilos personalizados en wwwroot/css/site.css para lograr un tema oscuro elegante, con:

Fondo principal casi negro / azul oscuro (#020617).

Tipografía clara (#e5e7eb) y acentos en celeste (#38bdf8).

Navbar y footer oscuros con texto claro.

Tablas a rayas en tonos oscuros.

Formularios con campos oscuros y bordes resaltados al enfocar.

Botones:

Primario azul (.btn-primary) para acciones principales.

Botón claro con borde (.btn-outline-light) para acciones secundarias.

La intención es que el sistema se vea como una agenda ejecutiva moderna, limpia y profesional.

8. Cómo ejecutar el proyecto

Clonar o descomprimir el proyecto

Descomprimir el archivo .zip en una carpeta local.

Abrir la solución GestionMetas.sln con Visual Studio 2022.

Configurar la cadena de conexión (si es necesario)

Abrir appsettings.json.

Verificar la sección:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=GestionMetasDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Ajustar el Server o Database si se usa otra instancia de SQL Server.

Aplicar migraciones (si la base de datos aún no existe)

Opción Visual Studio – Consola del Administrador de paquetes:

Herramientas → Administrador de paquetes NuGet → Consola del Administrador de paquetes.

Comando:

Update-Database


Esto creará la base de datos GestionMetasDb con las tablas Metas y Tareas.

Ejecutar la aplicación

Presionar F5 o hacer clic en el botón de “Iniciar depuración”.

El navegador abrirá la página de inicio (/).

Navegar utilizando el menú superior:

Inicio → panel principal.

Metas → CRUD de metas.

Tareas → CRUD de tareas.

Privacidad → vista simple de ejemplo.

9. Posibles mejoras futuras

Algunas ideas de extensión del proyecto:

Autenticación y autorización:

Que cada usuario tenga sus propias metas y tareas.

Filtros y ordenamientos avanzados:

Filtrar metas por estado, categoría o prioridad.

Ordenar tareas por fecha límite o dificultad.

Indicadores de avance:

Mostrar porcentaje de tareas completadas por meta.

Notificaciones:

Alertas para metas o tareas próximas a vencer.

API REST:

Exponer endpoints para consumir metas/tareas desde aplicaciones móviles.

10. Créditos y uso de herramientas de apoyo

El desarrollo de este proyecto fue realizado por:

Estudiante: [Aquí va tu nombre completo]

Curso: Programación II – II Cuatrimestre 2025.

Uso de inteligencia artificial como apoyo

Durante el desarrollo se utilizó ChatGPT (modelo GPT-5.1 Thinking de OpenAI) como herramienta de apoyo, exclusivamente en las siguientes formas:

Resolver dudas puntuales de sintaxis de C# y configuración de ASP.NET Core MVC.

Proponer ejemplos de código y estilos CSS, que luego fueron revisados, adaptados, integrados y probados manualmente por el estudiante.

Ayudar a redactar este archivo README y a organizar la explicación del proyecto.

Es importante dejar claro que:

El código final fue entendido, ajustado y probado por el estudiante.

No se copió ningún proyecto completo ni se descargó ninguna solución prehecha.

La IA se utilizó de manera similar a un tutor o referencia técnica, y no como autor único del trabajo.
