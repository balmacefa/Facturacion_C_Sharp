using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class ResumenFactura : IXMLGenerador
    {
        //      validates :exchange_rate, presence: true, if: -> { currency.present? }
        //      validate :totals_ok?

        private decimal totalServGravados;
        private decimal totalServExentos;
        private decimal totalMercanciasGravadas;
        private decimal totalMercanciasExentas;
        private decimal totalGravado;
        private decimal totalExento;
        private decimal totalVenta;
        private decimal totalDescuentos;
        private decimal totalVentaNeta;
        private decimal totalImpuesto;
        private decimal totalComprobante;
        private String codigoMoneda;
        private decimal tipoCambio;

        public ResumenFactura(decimal totalServGravados= 0,
                              decimal totalServExentos= 0,
                              decimal totalMercanciasGravadas= 0,
                              decimal totalMercanciasExentas= 0,
                              decimal totalGravado= 0,
                              decimal totalExento= 0,
                              decimal totalVenta= 0,
                              decimal totalDescuentos= 0,
                              decimal totalVentaNeta= 0,
                              decimal totalImpuesto= 0,
                              decimal totalComprobante= 0,
                              string codigoMoneda = "",
                              decimal tipoCambio=0)
        {
            this.totalServGravados = totalServGravados;
            this.totalServExentos = totalServExentos;
            this.totalMercanciasGravadas = totalMercanciasGravadas;
            this.totalMercanciasExentas = totalMercanciasExentas;
            this.totalGravado = totalGravado;
            this.totalExento = totalExento;
            this.totalVenta = totalVenta;
            this.totalDescuentos = totalDescuentos;
            this.totalVentaNeta = totalVentaNeta;
            this.totalImpuesto = totalImpuesto;
            this.totalComprobante = totalComprobante;
            this.codigoMoneda = codigoMoneda;
            this.tipoCambio = tipoCambio;
        }

        public decimal TotalServGravados { get => totalServGravados; set => totalServGravados = value; }
        public decimal TotalServExentos { get => totalServExentos; set => totalServExentos = value; }
        public decimal TotalMercanciasGravadas { get => totalMercanciasGravadas; set => totalMercanciasGravadas = value; }
        public decimal TotalMercanciasExentas { get => totalMercanciasExentas; set => totalMercanciasExentas = value; }
        public decimal TotalGravado { get => totalGravado; set => totalGravado = value; }
        public decimal TotalExento { get => totalExento; set => totalExento = value; }
        public decimal TotalVenta { get => totalVenta; set => totalVenta = value; }
        public decimal TotalDescuentos { get => totalDescuentos; set => totalDescuentos = value; }
        public decimal TotalVentaNeta { get => totalVentaNeta; set => totalVentaNeta = value; }
        public decimal TotalImpuesto { get => totalImpuesto; set => totalImpuesto = value; }
        public decimal TotalComprobante { get => totalComprobante; set => totalComprobante = value; }
        public string CodigoMoneda { get => codigoMoneda; set => codigoMoneda = value; }
        public decimal TipoCambio { get => tipoCambio; set => tipoCambio = value; }

        //        private bool ValidarTotales()
        //        {
        ////        errors[:taxable_total] << "invalid amount" unless @taxable_total == (@services_taxable_total + @goods_taxable_total).round(5)
        ////        errors[:exent_total] << "invalid amount" unless @exent_total == (@services_exent_total + @goods_exent_total).round(5)
        ////        errors[:subtotal] << "invalid amount" unless @subtotal == (@taxable_total + @exent_total).round(5)
        ////        errors[:gross_total] << "invalid amount" unless @gross_total == (@subtotal - @discount_total).round(5)
        ////        errors[:net_total] << "invalid amount" unless @net_total == (@gross_total + @tax_total).round(5)
        //}

        public XElement GenerarXML()
        {

            var baseXML = new XElement("ResumenFactura");

            if (!codigoMoneda.Equals(""))
            {
                baseXML.Add(new XElement("CodigoMoneda", codigoMoneda));
            }

            if (tipoCambio > 0)
            {
                baseXML.Add(new XElement("TipoCambio", tipoCambio));
            }

            baseXML.Add(new XElement("TotalServGravados", totalServGravados));
            baseXML.Add(new XElement("TotalServExentos", totalServExentos));
            baseXML.Add(new XElement("TotalMercanciasGravadas", totalMercanciasGravadas));
            baseXML.Add(new XElement("TotalMercanciasExentas", totalMercanciasExentas));
            baseXML.Add(new XElement("TotalGravado", totalGravado));
            baseXML.Add(new XElement("TotalExento", totalExento));
            baseXML.Add(new XElement("TotalVenta", totalVenta));
            baseXML.Add(new XElement("TotalDescuentos", totalDescuentos));
            baseXML.Add(new XElement("TotalVentaNeta", totalVentaNeta));
            baseXML.Add(new XElement("TotalImpuesto", totalImpuesto));
            baseXML.Add(new XElement("TotalComprobante", totalComprobante));

            return baseXML;

        }
    }
}