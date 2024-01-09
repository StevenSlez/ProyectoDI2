using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;

namespace ProyectoDI2;

public partial class MainWindow : Window
{
    //SoundPlayer temaso;
    //char musica = 'y';
    private List<Planeta> planetas;
    int num = 0;
    public MainWindow()
    {
        InitializeComponent();
        
        //string rutaCancion = "Imagenes\\StarWarsTheme.wav"; // le pongo temaso de StarWars al iniciar el programa (está directamente localizado en 'bin\Debug\net6.0-windows')
        //temaso = new SoundPlayer(rutaCancion);
        //temaso.PlayLooping();

        planetas = new List<Planeta>();

        //aquí cojo la ruta de las imagenes de los planetas precargados
        string rutaKashyyyk = "Imagenes\\kashyyyk_planet.png";
        string rutaTatooine = "Imagenes\\tatooine_planet.png";
        string rutaCoruscant = "Imagenes\\coruscant_planet.png";
        string rutaMandalore = "Imagenes\\mandalore_planet.png";
        string rutaBespin = "Imagenes\\bespin_planet.png";
        string rutaHoth = "Imagenes\\hoth_planet.png";

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

        btnAnterior.IsVisible = false; //como lo ubico en el primer planeta, inutilizo el boton Anterior
    }
    
    private void btnSiguiente_Click(object sender, EventArgs e) //para avanzar al planeta siguiente
    {
        num++; //avanzo en el contador del List
        if (num == planetas.Count - 1) //si me ubica en el último planeta, inutiliza el boton Siguiente (este)
            btnSiguiente.IsVisible = false;

        btnAnterior.IsVisible = true; //hago el boton Anterior visible
        mostrarDatos(); //muestro los datos del planeta actual
    }
    
    private void btnAnterior_Click(object sender, EventArgs e) //para retroceder al planeta anterior
    {
        num--; //retrocedo en el contador del List

        if (num == 0) //si me ubico en el primer planeta, inutilizo este boton Anterior
            btnAnterior.IsVisible = false;

        btnSiguiente.IsVisible = true; //habitilo el boton siguiente
        mostrarDatos(); //muestro los datos del planeta actual
    }

    private void btnEliminar_Click(object sender, EventArgs e) //para eliminar planetas
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

            string rutaDisparo = "Imagenes\\blaster.wav"; // le pongo temaso de StarWars al iniciar el programa (está directamente localizado en 'bin\Debug\net6.0-windows')
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
            //MostrarImagen(planetas[num].Imagen);

            lblActual.Text = "" + (num + 1);
            lblTotal.Text = "" + planetas.Count;
        }
    }
}