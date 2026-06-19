Feature: Gestión de Reservas (Bounded Context: Bookings)
  Como huésped del hotel boutique
  Quiero realizar una reserva de alojamiento
  Para asegurar mi habitación en las fechas seleccionadas

  Scenario: Crear una reserva de habitación de forma exitosa
    Given el huésped selecciona una habitación con ID 101 disponible
    And introduce sus datos de contacto con nombre "Mariana López" y correo "mariana@upc.pe"
    When envía la solicitud de confirmación de reserva desde el 2026-07-15 al 2026-07-20
    Then el sistema debe registrar la reserva en la base de datos de manera atómica
    And debe retornar el objeto de la reserva con los datos correspondientes