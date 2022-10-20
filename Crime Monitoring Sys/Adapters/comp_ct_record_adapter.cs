using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Crime_Monitoring_Sys.GetSet;
using System;
using System.Collections.Generic;

namespace Crime_Monitoring_Sys.Adapters
{
    internal class comp_ct_record_adapter : RecyclerView.Adapter
    {
        public event EventHandler<comp_ct_record_adapterClickEventArgs> ItemClick;
        public event EventHandler<comp_ct_record_adapterClickEventArgs> ItemLongClick;
        public event EventHandler<comp_ct_record_adapterClickEventArgs> deleteClick; 
        List<comp_records_ct> comp_Records_s;

        public comp_ct_record_adapter(List<comp_records_ct> data)
        {
            comp_Records_s = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_complaint_ct_layout, parent, false);

            var vh = new comp_ct_record_adapterViewHolder(itemView, OnClick, OnLongClick, OnDeleteClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {

            
            var holder = viewHolder as comp_ct_record_adapterViewHolder;
            holder.name.Text = comp_Records_s[position].comp_name;
            holder.compDesc.Text = comp_Records_s[position].comp_desc;
            holder.compType.Text = comp_Records_s[position].type_of_comp;
            holder.time_stamp.Text = comp_Records_s[position].time_stamp;
            holder.comp_stat.Text = comp_Records_s[position].comp_stat;
        }

        public override int ItemCount => comp_Records_s.Count;

        void OnClick(comp_ct_record_adapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(comp_ct_record_adapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnDeleteClick(comp_ct_record_adapterClickEventArgs args) => deleteClick?.Invoke(this, args);

    }

    public class comp_ct_record_adapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView name { get; set; }
        public TextView compDesc { get; set; }
        public TextView compType { get; set; }
        public TextView time_stamp { get; set; }
        public TextView  comp_stat { get; set; }
        public Button remove { get; set; }

        public comp_ct_record_adapterViewHolder(View itemView, Action<comp_ct_record_adapterClickEventArgs> clickListener,
                            Action<comp_ct_record_adapterClickEventArgs> longClickListener,
                            Action<comp_ct_record_adapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            //TextView = v;
            name = (TextView)itemView.FindViewById(Resource.Id.txtCompUserName);
            compDesc = (TextView)itemView.FindViewById(Resource.Id.txtCtCompDesc);
            compType = (TextView)itemView.FindViewById(Resource.Id.txtComType);
            time_stamp = (TextView)itemView.FindViewById(Resource.Id.compCtTimeStamp);
            comp_stat = (TextView)itemView.FindViewById(Resource.Id.txtCompStatus);
            remove = (Button)itemView.FindViewById(Resource.Id.btnRemoveComp);

            itemView.Click += (sender, e) => clickListener(new comp_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new comp_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            remove.Click += (sender, e) => deleteClickListener(new comp_ct_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class comp_ct_record_adapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}