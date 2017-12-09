% stdkalmandemo
T=100;  
Obj.phi=[1 1 ;0,1 ];  Obj.B=[1^2/2; 1];  Obj.gamma=Obj.B;
Obj.us=[1*ones(1,T/2) 0.5*ones(1,T/2)]; Obj.u=Obj.us(:,1);
Obj.Q=0.5;
Meter.H=[1 0]; Meter.R=1;

X1=[0; 2]; %初始状态
Z=PrepareData(X1,T, Obj, Meter);
Ks = stdkalman(Z,T, Obj, Meter); % Ks滤波参数虚类
newpts=[];
for i=1:length(Ks)
    newpts=[newpts Ks(i).X];
end
figure; hold on
plot(1:100, Z); % 原始数据
plot(2:100, newpts(1,:)); %滤波后的位置

% %plot(Z,'b+');%测量数据
% hold on;
% plot(xhat(1,:)-X(1,:),'r*');%滤波位置误差
% plot(Z-X(1,:),'yo');%测量位置误差
% hold off;
% figure;
% plot(diff(Z)/T,'y+');%根据测量数据计算的速度
% hold on;
% plot(xhat(2,:),'r*');%滤波速度
% plot(X(2,:),'bo');%真实速度
% hold off;
