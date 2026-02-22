Console.WriteLine("SISTEMA DE FACTURACION CON CONTROL ANTIFRAUDE");
Console.WriteLine("Tipo de cliente");
Console.WriteLine("1. Estudiante");
Console.WriteLine("2. Docente");
Console.WriteLine("3. Administrativo");
Console.WriteLine("4. Externo");
Console.WriteLine("Ingrese el tipo de cliente (1-4):");
int tipoCliente = int.Parse(Console.ReadLine());
Console.Write("Monto base: ");
string montoTxt = Console.ReadLine();
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
Console.WriteLine("\nReporte antifraude:");
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
double pctDescuento = 0.0;
