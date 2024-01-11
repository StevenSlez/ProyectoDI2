using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace ProyectoDI2;

public partial class MainWindow : Window
{
    //SoundPlayer temaso;
    //char musica = 'y';
    private List<Planeta> planetas;
    int num = 0;
    String ruta = "";
    OpenFileDialog ofd;
    string rutaData = "holocron.data";
    public MainWindow()
    {
        InitializeComponent();
        
        //string rutaCancion = "Imagenes\\StarWarsTheme.wav"; // le pongo temaso de StarWars al iniciar el programa (está directamente localizado en 'bin\Debug\net6.0-windows')
        //temaso = new SoundPlayer(rutaCancion);
        //temaso.PlayLooping();

        planetas = new List<Planeta>();

        //aquí cojo la ruta de las imagenes de los planetas precargados
        string rutaKashyyyk = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\kashyyyk_planet.png";
        string rutaTatooine = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\tatooine_planet.png";
        string rutaCoruscant = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\coruscant_planet.png";
        string rutaMandalore = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\mandalore_planet.png";
        string rutaBespin = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\bespin_planet.png";
        string rutaHoth = "C:\\Users\\sleza\\RiderProjects\\ProyectoDI2\\ProyectoDI2\\Imagenes\\hoth_planet.png";

        //cojo dichas rutas y convierto el archivo en un array de bytes
        byte[] iKashyyyk = File.ReadAllBytes(rutaKashyyyk);
        byte[] iTatooine = File.ReadAllBytes(rutaTatooine);
        byte[] iCoruscant = File.ReadAllBytes(rutaCoruscant);
        byte[] iMandalore = File.ReadAllBytes(rutaMandalore);
        byte[] iBespin = File.ReadAllBytes(rutaBespin);
        byte[] iHoth = File.ReadAllBytes(rutaHoth);

        //creo los objetos de la clase Planeta
        Planeta p1 = new Planeta("Kashyyyk", 56000, false, iKashyyyk);
        Planeta p2 = new Planeta("Tatooine", 67.2f, false, iTatooine);
        Planeta p3 = new Planeta("Coruscant", 313144, true, iCoruscant);
        Planeta p4 = new Planeta("Mandalore", 4000, true, iMandalore);
        Planeta p5 = new Planeta("Bespin", 6000, true, iBespin);
        Planeta p6 = new Planeta("Hoth", 0.1f, false, iHoth);

        //lo añado a la List
        planetas.Add(p1);
        planetas.Add(p2);
        planetas.Add(p3);
        planetas.Add(p4);
        planetas.Add(p5);
        planetas.Add(p6);
        
        mostrarDatos(); //carga este form directamente mostrando datos del primer planeta

        ofd = new OpenFileDialog();
        pbTik.IsVisible = false; //el "tik" lo invisibilizo 
        btnAnterior.IsVisible = false; //como lo ubico en el primer planeta, inutilizo el boton Anterior
    }
    
    private void btnSiguiente_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e) //para avanzar al planeta siguiente
    {
        num++; //avanzo en el contador del List
        if (num == planetas.Count - 1) //si me ubica en el último planeta, inutiliza el boton Siguiente (este)
            btnSiguiente.IsVisible = false;

        btnAnterior.IsVisible = true; //hago el boton Anterior visible
        mostrarDatos(); //muestro los datos del planeta actual
    }
    
    private void btnAnterior_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e) //para retroceder al planeta anterior
    {
        num--; //retrocedo en el contador del List

        if (num == 0) //si me ubico en el primer planeta, inutilizo este boton Anterior
            btnAnterior.IsVisible = false;

        btnSiguiente.IsVisible = true; //habitilo el boton siguiente
        mostrarDatos(); //muestro los datos del planeta actual
    }

    private void btnEliminar_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e) //para eliminar planetas
    {
        if (num >= 0 && num < planetas.Count) //para ver que estoy bien ubicado para eliminar un planeta
        {
            planetas.RemoveAt(num); //elimino el planeta de la List

            if (num >= planetas.Count) //para ubicarme en el siguiente planeta
            {
                num = planetas.Count - 1;
            }

            if (num == 0) //para intutilizar el boton anterior
            {
                btnAnterior.IsVisible = false;
            }

            if (num == planetas.Count - 1) //para inutilizar el boton siguiente
            {
                btnSiguiente.IsVisible = false;
            }

            //string rutaDisparo = "Imagenes\\blaster.wav"; // le pongo temaso de StarWars al iniciar el programa (está directamente localizado en 'bin\Debug\net6.0-windows')
            //blaster = new SoundPlayer(rutaDisparo);
            //blaster.Play();

            mostrarDatos(); //muestro los datos del planeta actual
        }
    }

    private void mostrarDatos() //metodo para mostrar datos que voy llamando cada 2x3
    {
        if (planetas.Count == 0) //si no hay planetas vacío los campos
        {
            txtNombre.Text = string.Empty;
            txtHabitantes.Text = string.Empty;
            txtImperialista.Text = string.Empty;
            //pbPlaneta.Image = null;

            lblActual.Text = "" + (num + 1);
            lblTotal.Text = "" + planetas.Count;
        }
        else //si hay planetas, muestro el que ubique el contador (num)
        {
            txtNombre.Text = planetas[num].Nombre;
            txtHabitantes.Text = planetas[num].Habitantes.ToString() + " K";
            if (planetas[num].Imperialista.Equals(true))
            {
                txtImperialista.Text = "Si";
            }
            else
            {
                txtImperialista.Text = "No";
            }
            MostrarImagen(planetas[num].Imagen);

            lblActual.Text = "" + (num + 1);
            lblTotal.Text = "" + planetas.Count;
        }
    }
    
    private void MostrarImagen(byte[] byteArray) //muestro la imagen del array de bytes
    {
        try
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Bitmap bitmap = new Bitmap(memoryStream);

                // asigno el Bitmap directamente al control Image
                pbPlaneta.Source = bitmap;
            }
        }
        catch (Exception ex)
        {
            PanelMensaje.IsVisible = true;
            ErrorText.FontSize = 11;
            ErrorText.Text = "Error al mostrar la imagen: \n" + ex.Message;
        }
    }
    
    private void btnModificar_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string nom, habS, imp;
        float hab;
        Boolean impB;

        nom = txtModNombre.Text; //recoge el dato del textbox
        if (!string.IsNullOrEmpty(nom)) //si no está vacío, continua
            planetas[num].Nombre = nom; //le asigna el nombre nuevo

        habS = txtModHab.Text; //recoge el dato del textbox
        if (!string.IsNullOrEmpty(habS)) //si no está vacío, continua
        {
            habS = habS.Replace('.', ','); //por si ponen '.', se lo cambio por ','
            hab = float.Parse(habS); //parseo a float
            planetas[num].Habitantes = hab; //asigno nueva cantidad
        }

        imp = txtModImp.Text; //recoge el dato del textbox
        if (!String.IsNullOrEmpty(imp)) //si no está vacío, continua
        {
            if (!imp.Equals("Si") && !imp.Equals("si") && !imp.Equals("No") && !imp.Equals("no")) //en caso de que se escriba otra cosa que no sea 'si' o 'no'
            {
                imp = "Si";
                PanelMensaje.IsVisible = true;
                ErrorText.FontSize = 11;
                ErrorText.Text = "No sabes escribir... escoria rebelde.\nAsumiremos que el planeta ES \nimperialista";
            }

            if (imp.Equals("Si") || imp.Equals("si"))
                impB = true;
            else
                impB = false;

            planetas[num].Imperialista = impB;
        }

        txtModNombre.Text = "";
        txtModHab.Text = "";
        txtModImp.Text = "";

        mostrarDatos();
    }
    
    private async void btnFoto_Click(object? sender, RoutedEventArgs routedEventArgs) //metodo para agregar imagenes con OpenFileDialog
    {
        ofd.Filters.Add(new FileDialogFilter { Name = "Imagenes PNG", Extensions = { "png" } });

        var result = await ofd.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            try
            {
                ruta = result[0]; // Obtiene la ruta del archivo seleccionado
                Console.WriteLine(ruta);
                pbTik.IsVisible = true; // Hace visible el control de la imagen
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    
    private void btnRegistrar(object sender, Avalonia.Interactivity.RoutedEventArgs e) //boton para registrar el planeta nuevo
    {
        byte[] iRuta;
        float habitantes;

        if (!ruta.Equals(""))
            iRuta = File.ReadAllBytes(ruta);
        else
            iRuta = null;

        String nombre = txtNombreNew.Text;
        String Shabitantes = txtHabNew.Text;

        if (!string.IsNullOrEmpty(Shabitantes))
        {
            Shabitantes = Shabitantes.Replace('.', ',');
            habitantes = float.Parse(Shabitantes);
        }
        else // si no se pone nada se entiende que tiene 0 habitantes
            habitantes = 0;

        String imperialista = txtImpNew.Text;
        if (!string.IsNullOrEmpty(imperialista) && (!imperialista.Equals("Si") || !imperialista.Equals("si") || !imperialista.Equals("No") || !imperialista.Equals("no"))) //en caso de que se escriba otra cosa que no sea 'si' o 'no'
        {
            imperialista = "Si";
            PanelMensaje.IsVisible = true;
            ErrorText.FontSize = 11;
            ErrorText.Text = "No sabes escribir... escoria rebelde.\nAsumiremos que el planeta ES \nimperialista";
        }

        //si alguno de los parametros se queda vacio o no se ha elegido png, salta el aviso en una ventana emergente
        if (!pbTik.IsVisible || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtHabitantes.Text) || string.IsNullOrEmpty(txtImperialista.Text))
        {
            PanelMensaje.IsVisible = true;
            ErrorText.Text = "Joven padawan, no puedes dejar ningún campo vacío.\n(Ni la imagen sin seleccionar)";
        }
        else //si están todos los datos correctos, creo el planeta nuevo
        {
            Planeta p;

            if (imperialista.Equals("Si") || imperialista.Equals("si"))
                p = new Planeta(nombre, habitantes, true, iRuta);
            else
                p = new Planeta(nombre, habitantes, false, iRuta);

            planetas.Add(p);

            txtNombreNew.Text = "";
            txtHabNew.Text = "";
            txtImpNew.Text = "";
            pbTik.IsVisible = false;
            ruta = "";
            
            //al registrar un planeta nuevo, vuelvo a llamar al metodo mostrarDatos para actualizar
            mostrarDatos();
        }
    }
    
    private void btnGuardar(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaData))
            {
                foreach (var data in planetas)
                {
                    writer.WriteLine(data.ToString());
                }
            }

            PanelMensaje.IsVisible = true;
            ErrorText.Text = "Joven padawan, se ha guardado \ncorrectamente los datos \nen el Holocrón.";
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error al guardar los datos: " + ex.Message);
        }
    }

    private void btnCargar(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            using (StreamReader reader = new StreamReader(rutaData))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    Planeta obj = ParsePlanetaFromString(line); //uso el metodo creado más adelante

                    if (obj != null)
                    {
                        planetas.Add(obj); // agrega el Planeta a la lista
                    }
                }

                PanelMensaje.IsVisible = true;
                ErrorText.Text = "Datos cargados correctamente \ndesde el Holocrón. \nQue no te lo quite los Sith!";
                
                mostrarDatos(); //actualizo los datos
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error al leer los datos: " + ex.Message);
        }
    }
    
    private Planeta ParsePlanetaFromString(string line)
    {
        string[] partes = line.Split('-'); //separa los elementos por cada '-' (al principio era por cada ',' pero generaba confusion a la hora de guardar el float decimal con su propia ',')

        // verifica si hay el número de elementos necesarios para crear un Planeta válido
        if (partes.Length >= 5)
        {
            string nombre = partes[1];
            float habitantes;
            float.TryParse(partes[2], out habitantes);
            bool imperialista = partes[3].Equals("True", StringComparison.OrdinalIgnoreCase);

            byte[] imagen;

            try
            {
                imagen = Convert.FromBase64String(partes[4]);
            }
            catch (FormatException ex)
            {
                // en caso de que el contenido no sea válido en formato Base64
                Console.WriteLine("Error al convertir la imagen de Base64 a bytes: " + ex.Message);
                return null;
            }

            Planeta planeta = new Planeta(nombre, habitantes, imperialista, imagen);

            mostrarDatos();
            lblTotal.Text = "" + (planetas.Count + 1); // le tengo que hacer esto porque al cargar mostrarDatos() me lo pone con 1 de menos...

            return planeta;
        }
        else
        {
            // si la línea no tiene los elementos suficientes para un Planeta, se devuelve null
            return null;
        }
    }
    
    private async void btnFatalError(object? sender, RoutedEventArgs routedEventArgs) //metodo que hace desaparecer el Panel del mensaje de error
    {
        PanelMensaje.IsVisible = false;
    }
}