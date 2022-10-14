namespace capaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.subCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.subProducto = new FontAwesome.Sharp.IconMenuItem();
            this.subNegocio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.subVentaRegistrar = new FontAwesome.Sharp.IconMenuItem();
            this.subVentaDetalle = new FontAwesome.Sharp.IconMenuItem();
            this.menuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.subCompraRegistrar = new FontAwesome.Sharp.IconMenuItem();
            this.subCompraDetalle = new FontAwesome.Sharp.IconMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.reporteVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteComprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTexto = new System.Windows.Forms.MenuStrip();
            this.contenedor = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            resources.ApplyResources(this.menu, "menu");
            this.menu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuMantenedor,
            this.menuVentas,
            this.menuCompras,
            this.menuProveedores,
            this.menuReportes});
            this.menu.Name = "menu";
            // 
            // menuUsuarios
            // 
            resources.ApplyResources(this.menuUsuarios, "menuUsuarios");
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuUsuarios.IconColor = System.Drawing.Color.Black;
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 40;
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuMantenedor
            // 
            resources.ApplyResources(this.menuMantenedor, "menuMantenedor");
            this.menuMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subCategoria,
            this.subProducto,
            this.subNegocio});
            this.menuMantenedor.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.menuMantenedor.IconColor = System.Drawing.Color.Black;
            this.menuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenedor.IconSize = 40;
            this.menuMantenedor.Name = "menuMantenedor";
            // 
            // subCategoria
            // 
            this.subCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subCategoria.IconColor = System.Drawing.Color.Black;
            this.subCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subCategoria.Name = "subCategoria";
            resources.ApplyResources(this.subCategoria, "subCategoria");
            this.subCategoria.Click += new System.EventHandler(this.subCategoria_Click);
            // 
            // subProducto
            // 
            this.subProducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subProducto.IconColor = System.Drawing.Color.Black;
            this.subProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subProducto.Name = "subProducto";
            resources.ApplyResources(this.subProducto, "subProducto");
            this.subProducto.Click += new System.EventHandler(this.subProducto_Click);
            // 
            // subNegocio
            // 
            this.subNegocio.Name = "subNegocio";
            resources.ApplyResources(this.subNegocio, "subNegocio");
            this.subNegocio.Click += new System.EventHandler(this.subNegocio_Click);
            // 
            // menuVentas
            // 
            resources.ApplyResources(this.menuVentas, "menuVentas");
            this.menuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subVentaRegistrar,
            this.subVentaDetalle});
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuVentas.IconColor = System.Drawing.Color.Black;
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 40;
            this.menuVentas.Name = "menuVentas";
            // 
            // subVentaRegistrar
            // 
            this.subVentaRegistrar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subVentaRegistrar.IconColor = System.Drawing.Color.Black;
            this.subVentaRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subVentaRegistrar.Name = "subVentaRegistrar";
            resources.ApplyResources(this.subVentaRegistrar, "subVentaRegistrar");
            this.subVentaRegistrar.Click += new System.EventHandler(this.subVentaRegistrar_Click);
            // 
            // subVentaDetalle
            // 
            this.subVentaDetalle.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subVentaDetalle.IconColor = System.Drawing.Color.Black;
            this.subVentaDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subVentaDetalle.Name = "subVentaDetalle";
            resources.ApplyResources(this.subVentaDetalle, "subVentaDetalle");
            this.subVentaDetalle.Click += new System.EventHandler(this.subVentaDetalle_Click);
            // 
            // menuCompras
            // 
            resources.ApplyResources(this.menuCompras, "menuCompras");
            this.menuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subCompraRegistrar,
            this.subCompraDetalle});
            this.menuCompras.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart;
            this.menuCompras.IconColor = System.Drawing.Color.Black;
            this.menuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompras.IconSize = 40;
            this.menuCompras.Name = "menuCompras";
            // 
            // subCompraRegistrar
            // 
            this.subCompraRegistrar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subCompraRegistrar.IconColor = System.Drawing.Color.Black;
            this.subCompraRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subCompraRegistrar.Name = "subCompraRegistrar";
            resources.ApplyResources(this.subCompraRegistrar, "subCompraRegistrar");
            this.subCompraRegistrar.Click += new System.EventHandler(this.subCompraRegistrar_Click);
            // 
            // subCompraDetalle
            // 
            this.subCompraDetalle.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subCompraDetalle.IconColor = System.Drawing.Color.Black;
            this.subCompraDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subCompraDetalle.Name = "subCompraDetalle";
            resources.ApplyResources(this.subCompraDetalle, "subCompraDetalle");
            this.subCompraDetalle.Click += new System.EventHandler(this.subCompraDetalle_Click);
            // 
            // menuProveedores
            // 
            resources.ApplyResources(this.menuProveedores, "menuProveedores");
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.DriversLicense;
            this.menuProveedores.IconColor = System.Drawing.Color.Black;
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 40;
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuReportes
            // 
            resources.ApplyResources(this.menuReportes, "menuReportes");
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteVentasToolStripMenuItem,
            this.reporteComprasToolStripMenuItem});
            this.menuReportes.IconChar = FontAwesome.Sharp.IconChar.Poll;
            this.menuReportes.IconColor = System.Drawing.Color.Black;
            this.menuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReportes.IconSize = 40;
            this.menuReportes.Name = "menuReportes";
            // 
            // reporteVentasToolStripMenuItem
            // 
            this.reporteVentasToolStripMenuItem.Name = "reporteVentasToolStripMenuItem";
            resources.ApplyResources(this.reporteVentasToolStripMenuItem, "reporteVentasToolStripMenuItem");
            this.reporteVentasToolStripMenuItem.Click += new System.EventHandler(this.reporteVentasToolStripMenuItem_Click);
            // 
            // reporteComprasToolStripMenuItem
            // 
            this.reporteComprasToolStripMenuItem.Name = "reporteComprasToolStripMenuItem";
            resources.ApplyResources(this.reporteComprasToolStripMenuItem, "reporteComprasToolStripMenuItem");
            this.reporteComprasToolStripMenuItem.Click += new System.EventHandler(this.reporteComprasToolStripMenuItem_Click);
            // 
            // menuTexto
            // 
            resources.ApplyResources(this.menuTexto, "menuTexto");
            this.menuTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(70)))), ((int)(((byte)(115)))));
            this.menuTexto.Name = "menuTexto";
            this.menuTexto.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuTexto_ItemClicked);
            // 
            // contenedor
            // 
            resources.ApplyResources(this.contenedor, "contenedor");
            this.contenedor.BackColor = System.Drawing.Color.Gainsboro;
            this.contenedor.Name = "contenedor";
            this.contenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.GhostWhite;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.iconPictureBox2);
            this.panel1.Controls.Add(this.lblUsuario);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.BackColor = System.Drawing.Color.GhostWhite;
            this.iconPictureBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.iconPictureBox2.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 21;
            resources.ApplyResources(this.iconPictureBox2, "iconPictureBox2");
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.TabStop = false;
            // 
            // lblUsuario
            // 
            resources.ApplyResources(this.lblUsuario, "lblUsuario");
            this.lblUsuario.BackColor = System.Drawing.Color.GhostWhite;
            this.lblUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblUsuario.Name = "lblUsuario";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(70)))), ((int)(((byte)(115)))));
            this.pictureBox1.Image = global::capaPresentacion.Properties.Resources.chispita1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.Coral;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.User;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlLightLight;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 24;
            resources.ApplyResources(this.iconPictureBox1, "iconPictureBox1");
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.TabStop = false;
            // 
            // Inicio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menuTexto);
            this.Controls.Add(this.iconPictureBox1);
            this.MainMenuStrip = this.menu;
            this.Name = "Inicio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.MenuStrip menuTexto;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuCompras;
        private FontAwesome.Sharp.IconMenuItem menuReportes;
        private FontAwesome.Sharp.IconMenuItem menuMantenedor;
        private System.Windows.Forms.Panel contenedor;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconMenuItem subCategoria;
        private FontAwesome.Sharp.IconMenuItem subProducto;
        private FontAwesome.Sharp.IconMenuItem subVentaRegistrar;
        private FontAwesome.Sharp.IconMenuItem subVentaDetalle;
        private FontAwesome.Sharp.IconMenuItem subCompraRegistrar;
        private FontAwesome.Sharp.IconMenuItem subCompraDetalle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUsuario;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private System.Windows.Forms.ToolStripMenuItem subNegocio;
        private System.Windows.Forms.ToolStripMenuItem reporteVentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteComprasToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

