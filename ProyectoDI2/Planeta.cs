using System;

namespace ProyectoDI2;

public class Planeta
{
    private static int num = 0;
    private int numero {  get; set; }
    private string nombre { get; set; }
    private float habitantes { get; set; }
    private bool imperialista { get; set; }
    private byte[] imagen { get; set; }
    public Planeta(string nombre, float habitantes, bool imperialista, byte[] imagen)
    {
        numero = ++num;
        this.nombre = nombre;
        this.habitantes = habitantes;
        this.imperialista = imperialista;
        this.imagen = imagen;
    }
    
    public int Numero
    {
        get { return numero; }
        set { numero = value; }
    }

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public float Habitantes
    {
        get { return habitantes; }
        set { habitantes = value; }
    }

    public bool Imperialista
    {
        get { return imperialista; }
        set { imperialista = value; }
    }

    public byte[] Imagen
    {
        get { return imagen; }
        set { imagen = value; }
    }

    public override string ToString()
    {
        string imagenBase64 = Convert.ToBase64String(imagen); // convierte la imagen a base64
        return $"{numero}-{nombre}-{habitantes}-{imperialista}-{imagenBase64}"; // (al principio era por cada ',' pero generaba confusion a la hora de guardar el float decimal con su propia ',')
    }
}