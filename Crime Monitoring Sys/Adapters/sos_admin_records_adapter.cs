using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Crime_Monitoring_Sys.GetSet;
using Java.Interop;
using System;
using System.Collections.Generic;

namespace Crime_Monitoring_Sys.Adapters
{
    internal class sos_admin_records_adapter : RecyclerView.Adapter
    {
        public event EventHandler<sos_admin_records_adapterClickEventArgs> ItemClick;
        public event EventHandler<sos_admin_records_adapterClickEventArgs> ItemLongClick;
        public event EventHandler<sos_admin_records_adapterClickEventArgs> traceClick;
        public event EventHandler<sos_admin_records_adapterClickEventArgs> updateClick;
        List<sos_records_admin> items;

        public sos_admin_records_adapter(List<sos_records_admin> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sos_list_admin_layout, parent, false);

            var vh = new sos_admin_records_adapterViewHolder(itemView, OnClick, OnLongClick, OnTraceClick, OnUpdateClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            
            var holder = viewHolder as sos_admin_records_adapterViewHolder;
            holder.usrname.Text = items[position].sos_name;
            holder.latlng.Text = items[position].sos_latitude + " , " +items[position].sos_longitude;
            holder.status.Text = items[position].sos_status;
            holder.time_stamp.Text = items[position].time_stamp;
        }

        public override int ItemCount => items.Count;

        void OnClick(sos_admin_records_adapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(sos_admin_records_adapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnTraceClick(sos_admin_records_adapterClickEventArgs args) => traceClick?.Invoke(this, args);
        void OnUpdateClick(sos_admin_records_adapterClickEventArgs args) => updateClick?.Invoke(this, args);

    }

    public class sos_admin_records_adapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView usrname { get; set; }
        public TextView latlng {get; set; }
        public TextView time_stamp { get; set; }
        public TextView status { get; set; }
        public Button update { get; set; }
        public Button trace { get; set; }


        public sos_admin_records_adapterViewHolder(View itemView, Action<sos_admin_records_adapterClickEventArgs> clickListener,
                            Action<sos_admin_records_adapterClickEventArgs> longClickListener, Action<sos_admin_records_adapterClickEventArgs> traceClickListener, Action<sos_admin_records_adapterClickEventArgs> updateClickListener) : base(itemView)
        {
            usrname = (TextView)itemView.FindViewById(Resource.Id.txtSosAdminUserName);
            latlng = (TextView)itemView.FindViewById(Resource.Id.txtSosAdminLngLat);
            time_stamp = (TextView)itemView.FindViewById(Resource.Id.txtSosAdminTime);
            status = (TextView)itemView.FindViewById(Resource.Id.txtSosAdminStatus);
            update = (Button)itemView.FindViewById(Resource.Id.btnUpdateSos);
            trace = (Button)itemView.FindViewById(Resource.Id.btnTraceSos);

            itemView.Click += (sender, e) => clickListener(new sos_admin_records_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new sos_admin_records_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            update.Click += (sender, e) => updateClickListener(new sos_admin_records_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            trace.Click += (sender, e) => traceClickListener(new sos_admin_records_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class sos_admin_records_adapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}