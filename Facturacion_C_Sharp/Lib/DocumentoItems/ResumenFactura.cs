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

        private float totalServGravados;
        private float totalServExentos;
        private float totalMercanciasGravadas;
        private float totalMercanciasExentas;
        private float totalGravado;
        private float totalExento;
        private float totalVenta;
        private float totalDescuentos;
        private float totalVentaNeta;
        private float totalImpuesto;
        private float totalComprobante;
        private String codigoMoneda;
        private float tipoCambio;

        public ResumenFactura(float totalServGravados= 0,
                              float totalServExentos= 0,
                              float totalMercanciasGravadas= 0,
                              float totalMercanciasExentas= 0,
                              float totalGravado= 0,
                              float totalExento= 0,
                              float totalVenta= 0,
                              float totalDescuentos= 0,
                              float totalVentaNeta= 0,
                              float totalImpuesto= 0,
                              float totalComprobante= 0,
                              string codigoMoneda = "",
                              float tipoCambio=0)
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

        public float TotalServGravados { get => totalServGravados; set => totalServGravados = value; }
        public float TotalServExentos { get => totalServExentos; set => totalServExentos = value; }
        public float TotalMercanciasGravadas { get => totalMercanciasGravadas; set => totalMercanciasGravadas = value; }
        public float TotalMercanciasExentas { get => totalMercanciasExentas; set => totalMercanciasExentas = value; }
        public float TotalGravado { get => totalGravado; set => totalGravado = value; }
        public float TotalExento { get => totalExento; set => totalExento = value; }
        public float TotalVenta { get => totalVenta; set => totalVenta = value; }
        public float TotalDescuentos { get => totalDescuentos; set => totalDescuentos = value; }
        public float TotalVentaNeta { get => totalVentaNeta; set => totalVentaNeta = value; }
        public float TotalImpuesto { get => totalImpuesto; set => totalImpuesto = value; }
        public float TotalComprobante { get => totalComprobante; set => totalComprobante = value; }
        public string CodigoMoneda { get => codigoMoneda; set => codigoMoneda = value; }
        public float TipoCambio { get => tipoCambio; set => tipoCambio = value; }

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

            if (tipoCambio > 0f)
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