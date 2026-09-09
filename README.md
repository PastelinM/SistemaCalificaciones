# Sistema de Calificaciones y Gestión Escolar

Aplicación de escritorio desarrollada en **C# WinForms**, diseñada para la administración integral de institutos, alumnos, materias, periodos escolares y evaluaciones. El sistema implementa una arquitectura limpia, desacoplada y escalable.

## Tecnologías y Herramientas

- **Lenguaje:** C# (.NET)
- **Interfaz de Usuario:** Windows Forms (WinForms)
- **Acceso a Datos:** Dapper (Micro-ORM)
- **Base de Datos:** PostgreSQL
- **Patrón de Arquitectura:** MVP (Model-View-Presenter) + Repository Pattern
- **Control de Versiones:** Git con metodología Gitflow

---

## Arquitectura del Proyecto

El proyecto está organizado bajo una estricta separación de responsabilidades:

```text
SistemaCalificaciones/
│
├── Core/
│   ├── Models/          # Entidades del dominio (Alumno, Materia, Periodo, etc.)
│   └── Interfaces/      # Contratos IFormView para desacoplar las vistas
│
├── Data/
│   ├── Config/          # Configuración y gestión de la conexión a la BD
│   ├── Repositories/    # CRUD con Dapper, mapeos explícitos y borrado lógico
│   └── Scripts/         # Scripts de inicialización y mantenimiento de la BD
│
├── Service/
│   ├── Calificaciones/  # Clases encargadas de ejecutar procesos de negocio
│   └── Validaciones/    # Clases para evaluar condiciones antes de impactar la BD
│
└── UI/
    ├── Presenters/      # Lógica de negocio, eventos y validaciones
    └── Views/           # Formularios WinForms (implementan IFormView)
