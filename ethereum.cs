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












// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract FileStorage {
    struct Reading {
        // Definir la estructura de datos para las lecturas
        // (asegúrate de que coincida con la estructura en tu aplicación)
        // Ejemplo: uint256 timestamp;
        //          uint256 value;
    }

    address public owner;
    mapping(address => bool) public authorizedUsers;

    // Array para almacenar las lecturas
    Reading[] public readings;

    event ReadingAdded(uint256 indexed index);

    constructor() {
        owner = msg.sender;
        authorizedUsers[msg.sender] = true;
    }

    modifier onlyOwner() {
        require(msg.sender == owner, "Solo el propietario puede realizar esta acción");
        _;
    }

    modifier onlyAuthorized() {
        require(authorizedUsers[msg.sender], "Usuario no autorizado");
        _;
    }

    function addReading(/* Parámetros de la lectura */) external onlyAuthorized {
        // Validar la lectura (puedes agregar lógica de validación aquí)

        // Agregar la lectura al contrato
        // Ejemplo: readings.push(Reading(/* Valores de la lectura */));

        // Emitir evento para indicar que se agregó una nueva lectura
        emit ReadingAdded(readings.length - 1);
    }

    function authorizeUser(address user) external onlyOwner {
        authorizedUsers[user] = true;
    }

    function revokeAuthorization(address user) external onlyOwner {
        authorizedUsers[user] = false;
    }

    // Implementa funciones para consultar las lecturas almacenadas en el contrato
    // Ejemplo: function getReadingCount() external view returns (uint256) {
    //             return readings.length;
    //          }
}
