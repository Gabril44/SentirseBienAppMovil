using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class PagoService
    {
        private readonly ConexionMysql conexionMysql;
        public ObservableCollection<Pago> Pagos { get; set; }

        public PagoService()
        {
            Pagos = new ObservableCollection<Pago>();
            conexionMysql = new ConexionMysql(); // Suponiendo que tienes una clase para la conexión
        }

        public async Task ReflejarPagoAsync(ObservableCollection<Pago> pagos, string metodoPagoSeleccionado)
        {
            using (MySqlConnection connection = conexionMysql.GetConnection())
            {
                await connection.OpenAsync();

                using (MySqlTransaction transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        string QUERY = "UPDATE pagos SET estado = @nuevoEstado, mediodepago = @nuevoMediodepago WHERE estado = 'pendiente' AND numeropago = @numeropago";

                        foreach (var pago in pagos)
                        {
                            using (MySqlCommand command = new MySqlCommand(QUERY, connection, transaction))
                            {
                                // Parámetros para la consulta SQL
                                command.Parameters.AddWithValue("@nuevoEstado", "pagado");
                                command.Parameters.AddWithValue("@numeropago", pago.nropago);
                                command.Parameters.AddWithValue("@nuevoMediodepago", metodoPagoSeleccionado);

                                int filasAfectadas = await command.ExecuteNonQueryAsync();

                                if (filasAfectadas > 0)
                                {
                                    // Mensaje de confirmación
                                    await Application.Current.MainPage.DisplayAlert("Éxito", $"El estado del pago '{pago.nropago}' ha sido actualizado a 'pagado'.", "OK");
                                }
                                else
                                {
                                    // Mensaje si no se encuentra el pago
                                    await Application.Current.MainPage.DisplayAlert("Atención", $"No se encontró el pago especificado: {pago.nropago}", "OK");
                                }
                            }
                        }

                        await transaction.CommitAsync(); // Confirma la transacción
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revierte la transacción en caso de error
                        await Application.Current.MainPage.DisplayAlert("Error", "Error al actualizar el estado del pago: " + ex.Message, "OK");
                    }
                }
            }
        }

        public async Task ReflejarUnPagoAsync(Pago pago, string metodoPagoSeleccionado)
        {
            using (MySqlConnection connection = conexionMysql.GetConnection())
            {
                //await connection.OpenAsync();

                using (MySqlTransaction transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        string QUERY = "UPDATE pagos SET estado = @nuevoEstado, mediodepago = @nuevoMediodepago WHERE estado = 'pendiente' AND numeropago = @numeropago";

                        
                            using (MySqlCommand command = new MySqlCommand(QUERY, connection, transaction))
                            {
                                // Parámetros para la consulta SQL
                                command.Parameters.AddWithValue("@nuevoEstado", "pagado");
                                command.Parameters.AddWithValue("@numeropago", pago.nropago);
                                command.Parameters.AddWithValue("@nuevoMediodepago", metodoPagoSeleccionado);

                                int filasAfectadas = await command.ExecuteNonQueryAsync();

                                if (filasAfectadas > 0)
                                {
                                    // Mensaje de confirmación
                                    await Application.Current.MainPage.DisplayAlert("Éxito", $"El estado del pago '{pago.nropago}' ha sido actualizado a 'pagado'.", "OK");
                                }
                                else
                                {
                                    // Mensaje si no se encuentra el pago
                                    await Application.Current.MainPage.DisplayAlert("Atención", $"No se encontró el pago especificado: {pago.nropago}", "OK");
                                }
                            }
                        

                        await transaction.CommitAsync(); // Confirma la transacción
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revierte la transacción en caso de error
                        await Application.Current.MainPage.DisplayAlert("Error", "Error al actualizar el estado del pago: " + ex.Message, "OK");
                    }
                }
            }
        }
    }
}
