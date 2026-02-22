Console.WriteLine("SISTEMA DE FACTURACION CON CONTROL ANTIFRAUDE");
Console.WriteLine("Tipo de cliente");
Console.WriteLine("1. Estudiante");
Console.WriteLine("2. Docente");
Console.WriteLine("3. Administrativo");
Console.WriteLine("4. Externo");
Console.WriteLine("Ingrese el tipo de cliente (1-4):");
int tipoCliente = int.Parse(Console.ReadLine());
Console.Write("Monto base ( use punto decimal ej. 1234.56): ");
double montoBase = double.Parse(Console.ReadLine());
Console.WriteLine("Método de pago:");
Console.WriteLine("1) Efectivo");
Console.WriteLine("2) Tarjeta");
Console.WriteLine("3) Transferencia");
Console.Write("Seleccione (1-3): ");
int metodo = int.Parse(Console.ReadLine());
Console.Write("¿Tiene cupón? (S/N): ");
string tieneCupon = Console.ReadLine().Trim().ToUpper();
string codigoCupon = "";
if (tieneCupon == "S")
{
    Console.Write("Ingrese código de cupón: ");
    codigoCupon = Console.ReadLine().Trim().ToUpper();
}
Console.WriteLine("Reporte antifraude:");
Console.WriteLine("1) Ninguno");
Console.WriteLine("2) Cupón inválido repetido");
Console.WriteLine("3) Pagos rechazados múltiples");
Console.Write("Seleccione (1-3): ");
int reporte = int.Parse(Console.ReadLine());

if (tipoCliente < 1 || tipoCliente > 4)
{
    Console.WriteLine("Error: Tipo de cliente fuera de rango (1-4).");
     return;
}
else if (montoBase < 0)
{
    Console.WriteLine("Error: El monto base no puede ser negativo.");
    return;
}
else if (metodo < 1 || metodo > 3)
{
    Console.WriteLine("Error: Método de pago fuera de rango (1-3).");
    return;
}
else if (tieneCupon != "S" && tieneCupon != "N")
{
    Console.WriteLine("Error: 'Tiene cupón' debe ser S o N.");
    return;
}
else if (tieneCupon == "S" && string.IsNullOrWhiteSpace(codigoCupon))
{
    Console.WriteLine("Error: Indicó que tiene cupón, pero no ingresó el código.");
    return;
}
else if (reporte < 1 || reporte > 3)
{
    Console.WriteLine("Error: Reporte antifraude fuera de rango (1-3).");
    return;
}

double pctDescBase = 0.0;

switch (tipoCliente)
{
    case 1: // Estudiante
        switch (metodo)
        {
            case 1: pctDescBase = 0.10; break; // Efectivo
            case 2: pctDescBase = 0.07; break; // Tarjeta
            case 3: pctDescBase = 0.05; break; // Transferencia
        }
        break;

    case 2: // Docente
        switch (metodo)
        {
            case 1: pctDescBase = 0.08; break;
            case 2: pctDescBase = 0.05; break;
            case 3: pctDescBase = 0.04; break;
        }
        break;

    case 3: // Administrativo
        switch (metodo)
        {
            case 1: pctDescBase = 0.06; break;
            case 2: pctDescBase = 0.04; break;
            case 3: pctDescBase = 0.03; break;
        }
        break;

    case 4: // Externo
        switch (metodo)
        {
            case 1: pctDescBase = 0.02; break;
            case 2: pctDescBase = 0.01; break;
            case 3: pctDescBase = 0.00; break;
        }
        break;
}
// Cupon válido con inicial U y que termine en numero par
double pctDescCupon = 0.0;
int cuponValido = 0;
if (tieneCupon == "S")
{
    string cod = codigoCupon;
    if (cod.Length >= 2)
    {
        string primera = cod.Substring(0, 1);
        string ultima = cod.Substring(cod.Length - 1);

        int digitoFinal = -1;
        if (primera == "U" && int.TryParse(ultima, out digitoFinal))
        {
            if (digitoFinal % 2 == 0)
            {
                cuponValido = 1;
            }   
        }
    }
    if (cuponValido == 1)
    {
        pctDescCupon = 0.05;
    }
    else
    {
        pctDescCupon = 0.0;
    }
}
// Calcular descuentos
double descuentoBase = montoBase * pctDescBase;
double descuentoCupon = montoBase * pctDescCupon;
double totalDescuento = descuentoBase + descuentoCupon;

//Atifraude
double multa = 0.0;
string mensajeFraude = "Ninguna";
if (reporte == 2)
{
    multa = montoBase * 0.10;
    mensajeFraude = "Cupón inválido repetido";
}
else if (reporte == 3)
{
    multa = montoBase * 0.20;
    mensajeFraude = "Pagos rechazados múltiples";
}
double recargo = montoBase * multa;

//Total
double totalPagar = montoBase - totalDescuento + recargo;

Console.WriteLine("*FACTURA*");
Console.WriteLine($"Monto base: Q{montoBase:0.00}");

Console.WriteLine("-DESCUENTOS APLICADOS");
Console.WriteLine($"Descuento por tipo de cliente y método de pago: Q{descuentoBase:0.00}");
if (tieneCupon == "S")
{
    if (cuponValido == 1)
    {
        Console.WriteLine($"Descuento por cupón (válido): Q{descuentoCupon:0.00}");
    }
    else
    {
        Console.WriteLine($"Descuento por cupón (inválido): Q{descuentoCupon:0.00}");
    }
}
else
{
    Console.WriteLine("No se aplicó descuento por cupón.");
}
Console.WriteLine("-MULTA ANTIFRAUDE");
if (reporte == 1)
{
    Console.WriteLine("No se aplicó multa antifraude.");
}
else
{
    Console.WriteLine($"Multa por reporte '{mensajeFraude}': Q{recargo:0.00}");
}
Console.WriteLine("*TOTAL A PAGAR*");
Console.WriteLine($"Q{totalPagar:0.00}");
// Ya no quiero :(