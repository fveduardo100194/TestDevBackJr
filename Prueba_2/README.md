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
