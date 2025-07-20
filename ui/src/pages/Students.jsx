import { useEffect, useState } from "react";
import api from "../services/api";
import { message, Spin, Table, Card } from "antd";

export default function Students() {
    const [loading, setLoading] = useState(false);
    const [students, setStudents] = useState([]);
    const [messageApi, contextHolder] = message.useMessage();

    useEffect(() => {
        const fetchStudents = async () => {
            try {
                setLoading(true);
                const response = await api.get("/api/v1/students");
                if (response.status === 200) {
                    messageApi.success("Students data fetched successfully.");
                    setStudents(response.data.result.result);
                    console.log(response.data);
                } else {
                    messageApi.error("Failed to fetch students data.");
                    console.log(response.data);
                }
            } catch (error) {
                messageApi.error("Error fetching students data: " + error.message);
                console.log(error);
            } finally {
                setLoading(false);
            }
        };
        fetchStudents();
    }, [messageApi]);


    const columns = [
        {
            title: "ID",
            dataIndex: "id",
            key: "id",
        },
        {
            title: "First Name",
            dataIndex: "firstName",
            key: "firstName",
        },
        {
            title: "Last Name",
            dataIndex: "lastName",
            key: "lastName",
        },
        {
            title: "Email",
            dataIndex: "email",
            key: "email",
        },
        {
            title: "Phone",
            dataIndex: "phone",
            key: "phone",
        },
    ];
    return (
        <>
        {contextHolder}
        <Card
            title="Students"
        >
            <Spin spinning={loading}>
                <Table
                    dataSource={students}
                    columns={columns}
                    rowKey="id"
                    pagination={{ pageSize: 10 }}
                />
            </Spin>
        </Card>
        </>
    );
    
}

