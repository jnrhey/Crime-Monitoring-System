
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using Crime_Monitoring_Sys.GetSet;
using AndroidX.RecyclerView.Widget;
using Android.Graphics;

namespace Crime_Monitoring_Sys.Adapters
{
    internal class sos_ct_record_adapter : RecyclerView.Adapter
    {
        public event EventHandler<sos_ct_record_adapterClickEventArgs> ItemClick;
        public event EventHandler<sos_ct_record_adapterClickEventArgs> ItemLongClick;
        public event EventHandler<sos_ct_record_adapterClickEventArgs> deleteClick;
        List<sos_records_ct> sos_Records;

        public sos_ct_record_adapter(List<sos_records_ct> data)
        {
            sos_Records = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sos_list_ct_model, parent, false);

            var vh = new sos_ct_record_adapterViewHolder(itemView, OnClick, OnLongClick,OnDeleteClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
         
            var holder = viewHolder as sos_ct_record_adapterViewHolder;
            holder.name.Text = sos_Records[position].sos_name;
            holder.ltlng.Text = sos_Records[position].sos_latitude + " , " + sos_Records[position].sos_longitude;
            holder.timeStamp.Text = sos_Records[position].time_stamp;
            if (sos_Records[position].sos_status == "Active")
            {
                holder.stats.Text = "Active";
                holder.stats.SetTextColor(Color.ParseColor("#FFBF00"));
            }
            else if (sos_Records[position].sos_status != "Active")
            {
                holder.stats.Text = "Solved";
                holder.stats.SetTextColor(Color.ParseColor("#51A944"));
            }


        }

        public override int ItemCount => sos_Records.Count;

        void OnClick(sos_ct_record_adapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(sos_ct_record_adapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnDeleteClick(sos_ct_record_adapterClickEventArgs args) => deleteClick?.Invoke(this, args);

    }

    public class sos_ct_record_adapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView name { get; set; }
        public TextView ltlng { get; set; }
        public TextView timeStamp { get; set; }
        public TextView stats { get; set; }
        public Button deleteBtn { get; set; }

        public sos_ct_record_adapterViewHolder(View itemView, Action<sos_ct_record_adapterClickEventArgs> clickListener,
                            Action<sos_ct_record_adapterClickEventArgs> longClickListener, Action<sos_ct_record_adapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            name = (TextView)itemView.FindViewById(Resource.Id.txtSosCtUserName);
            ltlng = (TextView)itemView.FindViewById(Resource.Id.txtSosCtAddress);
            timeStamp = (TextView)itemView.FindViewById(Resource.Id.sosCtTimeStamp);
            stats = (TextView)itemView.FindViewById(Resource.Id.txtSosCtStatus);
            deleteBtn = (Button)itemView.FindViewById(Resource.Id.btnRemoveSos);
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new sos_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new sos_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteBtn.Click += (sender, e) => deleteClickListener(new sos_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class sos_ct_record_adapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}