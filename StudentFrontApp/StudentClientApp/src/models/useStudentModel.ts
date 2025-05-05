import { request } from "@umijs/max";
import { Form } from "antd";
import OTP from "antd/es/input/OTP";
import { useRef, useState } from "react";

export default function useStudentModel() {
    const [open, setModalOpen] = useState(false);
    const [dataSource, setDataSource] = useState<any[]>([]);
    const [bounds, setBounds] = useState({ left: 0, top: 0, bottom: 0, right: 0 });
    const draggleRef = useRef<HTMLDivElement>(null!);
    const [groupOptions, setGroupOptions] = useState<any[]>();
    const [openEdit, setModalEditOpen] = useState(false);
    const [disabledEdit, setEditDisabled] = useState(true);
    const [formEdit] = Form.useForm();
    
    const setModalOpenState = (state: boolean) => {
        setModalOpen(state)
    }
    const setDataSourceState = (data: any[]) => {
        setDataSource(data)
    }
    const setGroupOptionsState = (data: any[]) => {
        setGroupOptions(data)
    }
    const setModalEditOpenState = (state: boolean) => {
        setModalEditOpen(state)
    }
    const setEditDisabledState = (state: boolean) => {
        setEditDisabled(state)
    }
    const loadGroups = () => {
        request('/api/Group/GetAll', { data: {}, method: 'GET', headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }}).then((result: any[]) => {

            const options = result.map((item: any) => ({ value: item.id, label: item.name}))
            console.log(options)
              setGroupOptions(options)
            });
    }
    const onEdit = (studentId:number) => {
        setModalEditOpen(true)
        request(`/api/Student/Get?id=${studentId}`, { data: {}, method: 'GET'}).then((result: any[]) => {
          formEdit.setFieldsValue(result)
        });
      }

    return {
        open,
        setModalOpenState,
        dataSource,
        setDataSourceState,
        bounds,
        setBounds,
        draggleRef,
        groupOptions,
        setGroupOptionsState,
        loadGroups,
        openEdit,
        disabledEdit,
        formEdit,
        setModalEditOpenState,
        setEditDisabledState,
        onEdit,
    }
}