% stdkalmandemo
T=100;  
Obj.phi=[1 1 ;0,1 ];  Obj.B=[1^2/2; 1];  Obj.gamma=Obj.B;
Obj.us=[1*ones(1,T/2) 0.5*ones(1,T/2)]; Obj.u=Obj.us(:,1);
Obj.Q=0.5;
Meter.H=[1 0]; Meter.R=1;

X1=[0; 2]; %��ʼ״̬
Z=PrepareData(X1,T, Obj, Meter);
Ks = stdkalman(Z,T, Obj, Meter); % Ks�˲���������
newpts=[];
for i=1:length(Ks)
    newpts=[newpts Ks(i).X];
end
figure; hold on
plot(1:100, Z); % ԭʼ����
plot(2:100, newpts(1,:)); %�˲����λ��

% %plot(Z,'b+');%��������
% hold on;
% plot(xhat(1,:)-X(1,:),'r*');%�˲�λ�����
% plot(Z-X(1,:),'yo');%����λ�����
% hold off;
% figure;
% plot(diff(Z)/T,'y+');%���ݲ������ݼ�����ٶ�
% hold on;
% plot(xhat(2,:),'r*');%�˲��ٶ�
% plot(X(2,:),'bo');%��ʵ�ٶ�
% hold off;
