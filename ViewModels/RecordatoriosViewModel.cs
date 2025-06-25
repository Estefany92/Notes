using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes.ViewModels
{
    internal partial class RecordatoriosViewModel : ObservableObject
    {

        private string filePath => Path.Combine(FileSystem.AppDataDirectory, "recordatorios.json");

        public ObservableCollection<Recordatorio> ListaRecordatorios { get; } = new();

        [ObservableProperty]
        private string nuevoTexto;

        [ObservableProperty]
        private DateTime fechaSeleccionada = DateTime.Today;

        [ObservableProperty]
        private TimeSpan horaSeleccionada = TimeSpan.Zero;

        //coreccion de error IA
        public RecordatoriosViewModel()
        {
            _ = CargarAsync();
        }

        [RelayCommand]
        private async Task AgregarAsync()
        {
            if (string.IsNullOrWhiteSpace(nuevoTexto))
                return;

            var fechaHora = fechaSeleccionada.Date + horaSeleccionada;
            if (fechaHora <= DateTime.Now)
                return;

            var nuevo = new Recordatorio
            {
                Texto = nuevoTexto,
                FechaHora = fechaHora,
                Activo = true
            };

            ListaRecordatorios.Add(nuevo);
            await GuardarAsync();

            nuevoTexto = string.Empty;
            fechaSeleccionada = DateTime.Today;
            horaSeleccionada = TimeSpan.Zero;
        }

        [RelayCommand]
        private async Task EliminarAsync(Recordatorio rec)
        {
            ListaRecordatorios.Remove(rec);
            await GuardarAsync();
        }

        private async Task GuardarAsync()
        {
            var json = JsonSerializer.Serialize(ListaRecordatorios.ToList(), new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        private async Task CargarAsync()
        {
            if (!File.Exists(filePath))
                return;

            var json = await File.ReadAllTextAsync(filePath);
            var lista = JsonSerializer.Deserialize<List<Recordatorio>>(json);
            if (lista != null)
            {
                foreach (var r in lista)
                    ListaRecordatorios.Add(r);
            }
        }
    }
}
