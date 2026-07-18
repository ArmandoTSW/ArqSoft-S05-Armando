# CitasApp: Arquitectura Hexagonal

Proyecto académico desarrollado para la materia de Arquitectura de Software. Esta aplicación implementa una arquitectura hexagonal de 4 capas con soporte para MVC y APIs REST.

## Descripción del Proyecto

El objetivo principal fue migrar una aplicación MVC tradicional hacia una arquitectura hexagonal para separar las responsabilidades de la lógica de negocio, el acceso a datos y la interfaz de usuario.

## Estructura de Capas

- CitasApp.Domain: Núcleo del negocio. Contiene las entidades y las interfaces de los repositorios.
- CitasApp.Application: Lógica de aplicación. Orquesta los casos de uso a través de servicios.
- CitasApp.Infrastructure: Adaptadores de salida. Implementa la persistencia de datos (JSON).
- CitasApp.Web: Adaptador de entrada (MVC). Interfaz gráfica para el usuario.

## APIs REST Implementadas

Se añadieron dos proyectos independientes para exponer servicios vía API:

1. CitasApp.api: Gestión de citas médicas (Pacientes, Médicos, Citas).
2. CitasApp.Api-Calculadora: Servicio de cálculo matemático.

## Patrones de Diseño

- Repository Pattern: Abstracción del acceso a datos.
- Dependency Injection: Registro de servicios en Program.cs.
- Service Layer: Orquestación centralizada de lógica.
- DTOs: Transferencia de datos desacoplada del modelo de dominio.

## Cómo Ejecutar

1. Clonar el repositorio:
   git clone https://github.com/ArmandoTSW/ArqSoft-S05-Armando.git

2. Abrir la solución CitasApp.sln en Visual Studio 2022.

3. Compilar la solución (Ctrl + Shift + B).

4. Configurar el proyecto de inicio según la necesidad:
   - Web MVC: CitasApp.Web
   - API Citas: CitasApp.api
   - API Calculadora: CitasApp.Api-Calculadora

## Estructura de Archivos

- CitasApp.Domain/
- CitasApp.Application/
- CitasApp.Infrastructure/
- CitasApp.Web/
- CitasApp.api/
- CitasApp.Api-Calculadora/

## Solución de Problemas Comunes

- Referencias faltantes: Asegurar que los proyectos tengan las dependencias correctas en Visual Studio.
- Swagger no carga: Verificar que los middlewares de Swagger estén configurados en Program.cs.
- CORS: Si los endpoints no responden desde el frontend, revisar la configuración de CORS en Program.cs.

## Cláusula de Uso de IA

Durante el desarrollo de CitasApp, se utilizó Gemini como herramienta de asistencia para:
- Diagnóstico de errores de compilación y referencias.
- Estructuración de la arquitectura hexagonal de 4 capas.
- Configuración de controladores y DTOs para las APIs REST.
- Refactorización de servicios e inyección de dependencias.

La responsabilidad final de la implementación, validación y decisiones arquitectónicas recae en el autor del proyecto.
