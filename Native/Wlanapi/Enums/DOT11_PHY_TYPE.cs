
namespace Native
{
    public enum DOT11_PHY_TYPE
    {
        /// <summary>
        /// Specifies an unknown or uninitialized PHY type.
        /// </summary>
        dot11_phy_type_unknown = 0,
        /// <summary>
        /// Specifies any PHY type.
        /// </summary>
        //dot11_phy_type_any = 0,
        /// <summary>
        /// Specifies a frequency-hopping spread-spectrum (FHSS) PHY.
        /// Bluetooth devices can use FHSS or an adaptation of FHSS.
        /// </summary>
        dot11_phy_type_fhss = 1,
        /// <summary>
        /// Specifies a direct sequence spread spectrum (DSSS) PHY type.
        /// </summary>
        dot11_phy_type_dsss = 2,
        /// <summary>
        /// Specifies an infrared (IR) baseband PHY type.
        /// </summary>
        dot11_phy_type_irbaseband = 3,
        /// <summary>
        /// Specifies an orthogonal frequency division multiplexing (OFDM) PHY type.
        /// 802.11a devices can use OFDM.
        /// </summary>
        dot11_phy_type_ofdm = 4,
        /// <summary>
        /// Specifies a high-rate DSSS (HRDSSS) PHY type.
        /// </summary>
        dot11_phy_type_hrdsss = 5,
        /// <summary>
        /// Specifies an extended rate PHY type (ERP). 802.11g devices can use ERP.
        /// </summary>
        dot11_phy_type_erp = 6,
        /// <summary>
        /// Specifies the 802.11n PHY type.
        /// </summary>
        dot11_phy_type_ht = 7,
        /// <summary>
        /// Specifies the 802.11ac PHY type. This is the very high throughput PHY type specified in IEEE 802.11ac.
        /// This value is supported on Windows 8.1, Windows Server 2012 R2, and later.
        /// </summary>
        dot11_phy_type_vht = 8,
        /// <summary>
        /// Specifies the start of the range that is used to define PHY types that are developed by an independent hardware vendor (IHV).
        /// </summary>
        dot11_phy_type_IHV_start = 0x8000,
        /// <summary>
        /// Specifies the start of the range that is used to define PHY types that are developed by an independent hardware vendor (IHV).
        ///</summary>
        dot11_phy_type_IHV_end = 0xFFFF,
    }
}
