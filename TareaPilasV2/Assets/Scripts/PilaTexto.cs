using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PilaTextos : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_Text textMostrarPila;
    public TMP_Text textMostrarTope;

    Stack<string> pilaNombres = new Stack<string>();


    public void PushString()
    {
        if (!string.IsNullOrEmpty(inputNombre.text))
        {
            pilaNombres.Push(inputNombre.text);
            inputNombre.text = "";

            string texto = "PILA:\n";
            bool esPrimero = true;
            foreach (string elemento in pilaNombres)
            {
                if (esPrimero)
                {
                    texto += " " + elemento + " \n";
                    esPrimero = false;
                }
                else
                {
                    texto += " " + elemento + " \n";
                }
            }
            textMostrarPila.text = texto;
            textMostrarTope.text = "Tope: ---";
        }
    }

    public void PeekString()
    {
        if (pilaNombres.Count > 0)
        {
            textMostrarTope.text = "Tope: " + pilaNombres.Peek();
        }
        else
        {
            textMostrarTope.text = "Pila vacía";
        }
    }

    public void PopString()
    {
        if (pilaNombres.Count > 0)
        {
            pilaNombres.Pop();

            
            if (pilaNombres.Count > 0)
            {
                string texto = "PILA:\n";
                bool esPrimero = true;
                foreach (string elemento in pilaNombres)
                {
                    if (esPrimero)
                    {
                        texto += " " + elemento + " \n";
                        esPrimero = false;
                    }
                    else
                    {
                        texto += " " + elemento + " \n";
                    }
                }
                textMostrarPila.text = texto;
            }
            else
            {
                textMostrarPila.text = "PILA:\n[VACÍA]";
            }

            textMostrarTope.text = "Tope: ---";
        }
    }

    public void ClearString()
    {
        pilaNombres.Clear();
        textMostrarPila.text = "PILA:\n[VACÍA]";
        textMostrarTope.text = "Tope: ---";
    }
}