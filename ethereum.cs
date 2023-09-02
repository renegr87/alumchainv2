using Nethereum.Web3; //esto ya esta ok

private async void bt_publish_Click(object sender, EventArgs e)
{
    try
    {
        var web3 = new Web3("tu_url_rpc_de_ethereum");  // Reemplaza con tu URL RPC de Ethereum
        var cuenta = new Account("tu_clave_privada"); // Reemplaza con la clave privada de tu cuenta de Ethereum

        // Convierte tus datos (Readings) a un formato de cadena que deseas almacenar en la cadena de bloques
        var datosParaPublicar = ConvertirReadingsAString(Readings);

        var transaccion = web3.Eth.GetEtherTransferService()
            .TransferEtherAndWaitForReceiptAsync(cuenta.Address, "tu_direccion_de_contrato", datosParaPublicar, new Nethereum.Hex.HexTypes.HexBigInteger(90000));

        lb_status.Text = "Transacción Exitosa"; // Actualiza la interfaz de usuario con el estado de la transacción
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
        lb_status.Text = "Transacción Fallida"; // Actualiza la interfaz de usuario con el estado de la transacción
    }
}