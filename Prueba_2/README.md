## Directorio para la prueba 1 ##

-- Schema testdevbackjr
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `testdevbackjr` DEFAULT CHARACTER SET utf8 COLLATE utf8_spanish_ci ;
USE `testdevbackjr` ;

-- -----------------------------------------------------
-- Table `testdevbackjr`.`usuarios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `testdevbackjr`.`usuarios` (
  `Login` VARCHAR(100) NOT NULL,
  `Nombre` VARCHAR(100) NULL,
  `Paterno` VARCHAR(100) NULL,
  `Materno` VARCHAR(100) NULL,
  PRIMARY KEY (`Login`))

-- -----------------------------------------------------
-- Table `testdevbackjr`.`empleados`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `testdevbackjr`.`empleados` (
  `empleadoId` INT NOT NULL AUTO_INCREMENT,
  `Usuario` VARCHAR(100) NOT NULL,
  `Sueldo` DOUBLE NULL,
  `FechaIngreso` DATE NULL,
  PRIMARY KEY (`empleadoId`),
  INDEX `fk_empleados_usuarios_idx` (`Usuario` ASC) VISIBLE,
  CONSTRAINT `fk_empleados_usuarios`
    FOREIGN KEY (`Usuario`)
    REFERENCES `testdevbackjr`.`usuarios` (`Login`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)

* Depurar solo los ID diferentes de 6,7,9 y 10  de la tabla  **usuarios** **_(5 puntos)_**

	R= 
		SELECT 
			*
		FROM usuarios
		WHERE
			usuarios.Login NOT IN ('user06' , 'user07', 'user09', 'user10');

* Actualizar el dato Sueldo en un 10 porciento a los empleados que tienen fechas entre el año 2000 y 2001 **_(5 puntos)_**

	R= 
		UPDATE empleados SET 
			empleados.Sueldo = empleados.Sueldo * 1.10
		WHERE EXTRACT(YEAR FROM FechaIngreso) BETWEEN '2000' AND '2001';

* Realiza una consulta para traer el nombre de usuario y fecha de ingreso de los usuarios que gananen mas de 10000 y su apellido comience con T ordernado del mas reciente al mas antiguo **_(10 puntos)_**

	R=
		SELECT
			usuarios.Nombre,
			empleados.FechaIngreso
		FROM usuarios
		LEFT JOIN empleados
			ON empleados.Usuario = usuarios.Login
		WHERE 
			empleados.Sueldo > 10000 AND usuarios.Paterno LIKE 'T%' 
		ORDER BY 
			empleados.FechaIngreso DESC


* Realiza una consulta donde agrupes a los empleados por sueldo, un grupo con los que ganan menos de 1200 y uno mayor o igual a 1200, cuantos hay en cada grupo? **_(10 puntos)_**

	R=
		SELECT
			SUM(
				CASE 
					WHEN empleados.Sueldo < 1200 THEN 1 ELSE 0
				END
			) AS '< 1200',
			SUM(
				CASE 
					WHEN empleados.Sueldo >= 1200 THEN 1 ELSE 0
				END
			) AS '>= 1200'
		FROM empleados;
